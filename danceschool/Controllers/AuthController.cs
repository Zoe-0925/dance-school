using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using danceschool.Models;
using System.Collections.Generic;
using danceschool.Helpers;
using danceschool.Api;
using danceschool.Handlers.CommandHandlers;
using Serilog;

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

                var claims = new Dictionary<string, object>()
                 {
                     { "student", true }
                 };

                Log.Information("Sign up(1/2): Created a new student-role Firebase user.");
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);
                BaseResponse<int> result = (BaseResponse<int>)await Mediator.Send(new RegisterStudentCommand { UserName = request.UserName, Email = request.Email });
                Log.Information("Sign up(2/2): completed.");
                return Ok(new { Successful = true }); // IsCached = false
            }
            catch (Exception e)
            {
                Log.Error($"401 Error in Auth Controller: SignUp(). {e.Message}");
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
                {
                    Log.Error("401 Error in Auth Controller: SignUp(). ID token has expired");
                    return StatusCode(401, new { Error = "ID token has expired" });
                }
                else if (ex.AuthErrorCode == AuthErrorCode.InvalidIdToken)
                {
                    Log.Error("401 Error in Auth Controller: SignUp(). ID token is malformed or invalid");
                    return StatusCode(401, new { Error = "ID token is malformed or invalid" });
                }
                else
                {
                    Log.Error("401 Error in Auth Controller: SignUp(). Failed to verify ID token.");
                    return StatusCode(400, new { Error = "Failed to verify ID token." });
                }
            }
            catch (Exception ex)
            {
                Log.Error($"500 Error in Auth Controller: VerifyToken(). {ex.Message}");
                return StatusCode(500, ex);
            }
        }
    }
}