using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using danceschool.Models;
using System.Collections.Generic;
using danceschool.Helpers;
using danceschool.Api;
using danceschool.Handlers.CommandHandlers;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller that handles authentication
    /// </summary>
    [Route("Account")]
    public class AuthController : BaseController
    {

        /// <summary>
        /// Sign Up a new user (a student or instructor) to Firebase
        /// </summary>
        /// <response code="401">If the user email already exists in Firebase</response>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                UserRecordArgs args = new UserRecordArgs()
                {
                    Email = request.Email,
                    EmailVerified = true,
                    Password = request.Password,
                    DisplayName = request.UserName,
                    Disabled = false,
                };
                Console.WriteLine("request.email", request.Email);

                var claims = new Dictionary<string, object>()
                 {
                     { "student", true }
                 };

                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);
                var token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userRecord.Uid);

                BaseResponse<int> result = (BaseResponse<int>)await Mediator.Send(new RegisterStudentCommand { UserName = request.UserName, Email = request.Email });

                return Ok(new SignUpResult()
                {
                    Succeeded = true
                });
            }
            catch (Exception e)
            {
                return StatusCode(401, new { Error = e.Message });
            }
        }

        /// <summary>
        /// Verify the ID token from the client
        /// </summary>
        /// <response code="400">If the Firebase id token is invalid or expired</response>
        /// <response code="401">If the token validation failed</response>
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken([FromBody] Token idToken)
        {
            try
            {
                
                string role = await AuthHelper.GetRoleFromTokenAsync(idToken.IdToken);
                return Ok(role);
            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine(ex);
                if (ex.AuthErrorCode == AuthErrorCode.ExpiredIdToken)
                    return StatusCode(401, new { Error = "ID token has expired" });
                else if (ex.AuthErrorCode == AuthErrorCode.InvalidIdToken)
                    return StatusCode(401, new { Error = "ID token is malformed or invalid" });
                else
                    return StatusCode(400, new { Error = "Failed to verify ID token." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex);
            }
        }
    }
}