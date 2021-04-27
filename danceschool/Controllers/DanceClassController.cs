using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Helpers;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using danceschool.Filter;
using Microsoft.AspNetCore.Mvc;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System;
using danceschool.Models;
using Serilog;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the Model "Dance Class"
    /// </summary>
    /// <response code="401">If the Firebase Authentication or Authorization failed</response>
    [Route("api/classes")]
    public class DanceClassController : BaseController
    {
        /// <summary>
        /// Get all Dance Classes
        /// </summary>
        // GET: api/classes/course/5
        [HttpGet("course/{id}/page/{PageNumber}/size/{PageSize}/upcoming/{Upcoming}")]
        public async Task<IActionResult> GetByCourse(int id, int PageNumber, int PageSize, bool Upcoming)
        {
            var validFilter = new PaginationFilter(PageNumber, PageSize);

            IEnumerable<DanceClassDTO> data = new List<DanceClassDTO>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_danceclasses_by_courses_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<DanceClassDTO>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<DanceClassDTO>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetClassesByCourseQuery { Id = id, PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize, Upcoming = Upcoming });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<DanceClassDTO>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_danceclasses_by_courses_" + validFilter.PageNumber, cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Create a new Dance Classes
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="500">If the database update failed</response>
        // POST api/classes
        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in DanceClass Controller: CreateClass()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception e)
            {
                Log.Error($"Database create Error. {e} in DanceClass Controller");
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Update a particular Dance Classes
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Dance class to be updated is not found</response>
        /// <response code="500">If the database update failed</response>
        // PUT api/classes/5
        [HttpPut]
        public async Task<IActionResult> UpdateClass(UpdateClassCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in DanceClass Controller: UpdateClass()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception e)
            {
                Log.Error($"Database update Error. {e} in DanceClass Controller");
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Delete a particular Dance Classes
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Dance class to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in DanceClass Controller: Delete()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                var result = await Mediator.Send(new DeleteClassCommand { Id = id });
                if (!result.Success)
                {
                    Log.Error($"{result.Error.StatusCode} Error. {result.Error} in Instructor Controller: UnregisterInstructor()");
                    return StatusCode(result.Error.StatusCode, result.Error);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error($"Database delete Error. {e} in DanceClass Controller");
                return StatusCode(500, e);
            }
        }
    }
}
