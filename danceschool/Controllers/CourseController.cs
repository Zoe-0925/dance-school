﻿using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Helpers;
using danceschool.Filter;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using Microsoft.AspNetCore.Mvc;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using danceschool.Models;
using System;
using Serilog;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the Model "Course"
    /// </summary>
    [Route("api/courses")]
    public class CourseController : BaseController
    {
        /// <summary>
        /// Get all courses with pagination
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid</response>
        /// <response code="404">If the page of the data is not found</response>
        // GET: api/courses/
        [HttpGet("page/{PageNumber}/size/{PageSize}")]
        public async Task<IActionResult> Get(int PageNumber, int PageSize)
        {
            var validFilter = new PaginationFilter(PageNumber, PageSize);

            IEnumerable<CourseDTO> data = new List<CourseDTO>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_courses_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<CourseDTO>>(cachedDataString);
<<<<<<< HEAD
                Log.Information("Successfully found cache results of courses.");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return Ok(new BaseResponse<IEnumerable<CourseDTO>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetCourseQuery { PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<CourseDTO>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_courses_" + validFilter.PageNumber, cachedDataString);
<<<<<<< HEAD
                Log.Information("Successfully found courses and saved to cache.");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Get all courses with the count of the course and pagination
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="404">If the page of the data is not found</response>
        // GET: api/courses/
        [HttpGet("page/{PageNumber}/size/{PageSize}/count")]
        public async Task<IActionResult> GetWithCount(int PageNumber, int PageSize)
        {
            var validFilter = new PaginationFilter(PageNumber, PageSize);

            CourseWithCountDTO data = new CourseWithCountDTO();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_courses_with_count_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<CourseWithCountDTO>(cachedDataString);
<<<<<<< HEAD
                Log.Information("Successfully found cache results of course with count.");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return Ok(new BaseResponse<CourseWithCountDTO>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetCourseWithCountQuery { PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<CourseWithCountDTO>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_courses_with_count_" + validFilter.PageNumber, cachedDataString);
<<<<<<< HEAD
                Log.Information("Successfully found courses with count and saved to cache.");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Search courses by name
        /// </summary>
        // GET: api/courses/search/Bachata
        [HttpGet("search/{Query}")]
        public async Task<IActionResult> SearchCourseByName(string Query)
        {
<<<<<<< HEAD
=======

            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Course Controller: SearchCourseByName()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }

>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            IEnumerable<CourseDTO> data = new List<CourseDTO>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_courses_search_" + Query);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<CourseDTO>>(cachedDataString);
<<<<<<< HEAD
                Log.Information("Successfully found cache results of course by name.");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return Ok(new BaseResponse<IEnumerable<CourseDTO>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetCourseByNameQuery { Query = Query });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<CourseDTO>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_courses_search_" + Query, cachedDataString);
<<<<<<< HEAD
                Log.Information("Successfully found the course by name and saved to cache");
=======
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Create a new course
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="500">If the database update failed</response>
        // POST api/courses
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
        {
<<<<<<< HEAD
=======


>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
<<<<<<< HEAD
                Log.Error($"Failed to create the course. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            Log.Information($"Successfully created the course of id:{result.Data}.");
            return Ok(result);
=======
                Log.Error("401 Error. Unauthorized in Course Controller: CreateCourse()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception e)
            {
                Log.Error($"Database create Error. {e} in Course Controller");
                return StatusCode(500, e);
            }
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
        }

        /// <summary>
        /// Update a particular course
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Course to be updated is not found</response>
        /// <response code="500">If the database update failed</response>
        // PUT api/courses/5
        [HttpPut]
        public async Task<IActionResult> UpdateCourse(UpdateCourseCommand command)
        {
<<<<<<< HEAD
=======


>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
<<<<<<< HEAD
                Log.Error($"Failed to update the course of id: {command.Id}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                Log.Information($"Successfully updated the course of id: {command.Id}");
                return Ok(result);
            }
            Log.Error($"Failed to update the course of id: {command.Id}. {result.Error.StatusCode} Error. {result.Error} in Course Controller: Delete()");
            return StatusCode(result.Error.StatusCode, result.Error);
=======
                Log.Error("401 Error. Unauthorized in Course Controller: UpdateCourse()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception e)
            {
                Log.Error($"Database update Error. {e} in Course Controller");
                return StatusCode(500, e);
            }
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
        }

        /// <summary>
        /// Delete a particular course
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Course to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
<<<<<<< HEAD
=======


>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
<<<<<<< HEAD
                Log.Error($"Failed to delete the course of id: {id}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(new DeleteCourseCommand { Id = id });
            if (!result.Success)
            {
                Log.Error($"Failed to delete the course of id: {id}. {result.Error.StatusCode} Error. {result.Error}.");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully deleted the course of id: {id}");
            return Ok(result);
=======
                Log.Error("401 Error. Unauthorized in Course Controller: Delete()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                var result = await Mediator.Send(new DeleteCourseCommand { Id = id });
                if (!result.Success)
                {
                    Log.Error($"{result.Error.StatusCode} Error. {result.Error} in Course Controller: Delete()");
                    return StatusCode(result.Error.StatusCode, result.Error);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error($"Database delete Error. {e} in Course Controller: Delete");
                return StatusCode(500, e);
            }
>>>>>>> 6932947c1096e40a2211381a7ba1a25ec95a0c4f
        }
    }
}
