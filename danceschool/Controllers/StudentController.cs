using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using danceschool.Helpers;
using danceschool.Models;
using danceschool.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Serilog;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the Model "Membership"
    /// </summary>
    [Route("api/students")]
    public class StudentController : BaseController
    {

        /// <summary>
        /// Get all Students
        /// </summary>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        // GET: api/students/
        [HttpGet("page/{PageNumber}/size/{PageSize}/")]
        public async Task<IActionResult> Get(int PageNumber, int PageSize)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to get students. 401 Error.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var validFilter = new PaginationFilter(PageNumber, PageSize);

            IEnumerable<StudentDTO> data = new List<StudentDTO>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_students_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<StudentDTO>>(cachedDataString);
                Log.Information($"Successfully got the cached students.");
                return Ok(new BaseResponse<IEnumerable<StudentDTO>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetStudentQuery { PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                if (baseResponse.Success)
                {
                    data = baseResponse.Data;
                    cachedDataString = JsonSerializer.Serialize<IEnumerable<StudentDTO>>(baseResponse.Data);
                    var expiryOptions = new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                        SlidingExpiration = TimeSpan.FromSeconds(300)
                    };
                    await DistributedCache.SetStringAsync("_students_" + validFilter.PageNumber, cachedDataString);
                    Log.Information($"Successfully got the students and saved to cache.");
                    return Ok(baseResponse); // IsCached = false
                }
                Log.Error($"Failed to get students. ${baseResponse.Error.StatusCode} Error. {baseResponse.Error.Message}");
                return StatusCode(baseResponse.Error.StatusCode, baseResponse.Error);
            }
        }

        /// <summary>
        /// Search students by name
        /// </summary>
        // GET: api/students/search/Mario
        [HttpGet("search/{Query}")]
        public async Task<IActionResult> SearchStudentByName(string Query)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to search students. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            IEnumerable<StudentDTO> data = new List<StudentDTO>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_students_search_" + Query);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<StudentDTO>>(cachedDataString);
                Log.Information($"Successfully found the cached students of name:{Query}.");
                return Ok(new BaseResponse<IEnumerable<StudentDTO>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetStudentByNameQuery { Query = Query });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<StudentDTO>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_students_search_" + Query, cachedDataString);
                Log.Information($"Successfully found the students of name:{Query} and saved to cache.");
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Get a particular student
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the student is not found</response>
        /// <response code="500">If the database update failed</response>
        // GET: api/students/test@gmail.com
        [HttpGet("{email}")]
        public async Task<IActionResult> GetSingleStudent(string email)
        {
            StudentDTO data = new StudentDTO();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_students_email_" + email);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<StudentDTO>(cachedDataString);
                Log.Information($"Successfully find the cached student of email:{email}.");
                return Ok(new BaseResponse<StudentDTO>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetSingleStudentQuery { Email = email });
                if (baseResponse.Success)
                {
                    // loading from code (in real-time from database)
                    // then saving to the redis cache 
                    data = baseResponse.Data;
                    cachedDataString = JsonSerializer.Serialize<StudentDTO>(baseResponse.Data);
                    var expiryOptions = new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                        SlidingExpiration = TimeSpan.FromSeconds(300)
                    };
                    await DistributedCache.SetStringAsync("_students_email_" + email, cachedDataString);
                    Log.Information($"Successfully find the student of email:{email} and saved to cache.");
                    return Ok(baseResponse); // IsCached = false
                }
                Log.Error($"Failed to find student of email:{email}. 401 Error. Unauthorized.");
                return StatusCode(baseResponse.Error.StatusCode, baseResponse.Error);
            }
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="500">If the database update failed</response>
        // POST api/students/
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegisterStudentCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to create student. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            Log.Information($"Successfully created the student of id:{result.Data}.");
            return Ok(result);
        }

        /// <summary>
        /// Update a particular student
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Student to be updated is not found</response>
        /// <response code="500">If the database update failed</response>
        // PUT api/students//userName
        [HttpPut]
        public async Task<IActionResult> UpdateStudentUserName(UpdateStudentUserNameCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error($"Failed to update the student of id:{command.Id}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                Log.Error($"Failed to update the student of id:{command.Id}. {result.Error.StatusCode} Error. {result.Error}.");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully updated the student of id:{command.Id}.");
            return Ok(result);
        }

        /// <summary>
        /// Delete a particular student
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Student to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/students/test@gmail.com
        [HttpDelete("{Id}")]
        public async Task<IActionResult> UnregisterStudent(int Id)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error($"Failed to delete the student of id:{Id}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(new UnregisterStudentCommand { StudentID = Id });

            if (!result.Success)
            {
                Log.Error($"Failed to delete the student of id:{Id}. {result.Error.StatusCode} Error. {result.Error}.");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully deleted the student of id:{Id}.");
            return Ok(result);
        }
    }
}
