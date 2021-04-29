using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Helpers;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using Microsoft.AspNetCore.Mvc;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Collections.Generic;
using danceschool.Models;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Serilog;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the Model "Instructor"
    /// </summary>
    [Route("api/instructors")]
    public class InstructorController : BaseController
    {

        /// <summary>
        /// Get all instructors
        /// </summary>
        // GET: api/instructors/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Instructor> data = new List<Instructor>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_instrutcors");
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                data = JsonSerializer.Deserialize<IEnumerable<Instructor>>(cachedDataString);
                Log.Information($"Successfully found cached instructors.");
                return Ok(new BaseResponse<IEnumerable<Instructor>>(data, true)); // IsCached = true
            }
            else
            {
                BaseResponse<IEnumerable<Instructor>> baseResponse = await Mediator.Send(new GetInstructorQuery());
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<Instructor>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_instructors", cachedDataString);
                Log.Information($"Successfully found the instructors and saved to cache.");
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Search instructors by name
        /// </summary>
        // GET: api/instructors/search/Mario
        [HttpGet("search/{Query}")]
        public async Task<IActionResult> SearchInstructorByName(string Query)
        {

            IEnumerable<Instructor> data = new List<Instructor>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_instructors_search_" + Query);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                data = JsonSerializer.Deserialize<IEnumerable<Instructor>>(cachedDataString);
                Log.Information($"Successfully found cached instructors of name:{Query}.");
                return Ok(new BaseResponse<IEnumerable<Instructor>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetInstructorByNameQuery { Query = Query });
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<Instructor>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_instructors_search_" + Query, cachedDataString);
                Log.Information($"Successfully found the instructors of name:{Query} and saved to cache.");
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Create a new instructor
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="500">If the database update failed</response>
        // POST api/instructors
        [HttpPost]
        public async Task<IActionResult> RegisterInstructor(RegisterInstructorCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to create the instructor. 401 Error. Unauthorized in Instructor Controller: RegisterInstructor()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            Log.Information($"Successfully created the instructor of id:{result.Data}.");
            return Ok(result);
        }

        /// <summary>
        /// Update a particular instructor
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Instructor to be updated is not found</response>
        /// <response code="500">If the database update failed</response>
        // PUT api/instructors/
        [HttpPut]
        public async Task<IActionResult> UpdateInstructor(UpdateInstructorCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to update the instructor. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                Log.Error($"Failed to update the instructor. {result.Error.StatusCode} Error. {result.Error} in Instructor Controller: UpdateInstructor()");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete a particular instructor
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Instructor to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/instructors/test@gmail.com
        [HttpDelete("{id}")]
        public async Task<IActionResult> UnregisterInstructor(int id)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to delete the instructor. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(new UnregisterInstructorCommand { Id = id });
            if (!result.Success)
            {
                Log.Error($"Failed to delete the instructor. {result.Error.StatusCode} Error. {result.Error}.");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully deleted the instructor of id:{id}.");
            return Ok(result);
        }
    }
}
