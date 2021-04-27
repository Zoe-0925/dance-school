using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using danceschool.Helpers;
using danceschool.Filter;
using Microsoft.AspNetCore.Mvc;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System;
using danceschool.Models;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace danceschool.Controllers
{
    /// <summary>
    /// The base controller for Model "Booking"
    /// </summary>
    /// <response code="401">If the Firebase Authentication or Authorization failed</response>
    [Route("api/bookings")]
    public class BookingController : BaseController
    {
        /// <summary>
        /// Get all bookings with count
        /// </summary>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        // GET: api/bookings/page/5/size/10/count
        [HttpGet("page/{PageNumber}/size/{PageSize}/")]
        public async Task<IActionResult> Get(int PageNumber, int PageSize)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);

            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Analytical Controller: Get()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }

            Log.Information("Getting all bookings.");
            var validFilter = new PaginationFilter(PageNumber, PageSize);

            IEnumerable<BookingDTO> data = new List<BookingDTO>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_bookings_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                Log.Information("Returned cached results.");
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<BookingDTO>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<BookingDTO>>(data, true)); // IsCached = true
            }
            else
            {
                Log.Information("Cached new results.");
                BaseResponse<IEnumerable<BookingDTO>> baseResponse = await Mediator.Send(new GetBookingQuery { PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<BookingDTO>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_bookings_" + validFilter.PageNumber, cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Get all bookings with count
        /// </summary>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        // GET: api/bookings/page/5/size/10/count
        [HttpGet("page/{PageNumber}/size/{PageSize}/count")]
        public async Task<IActionResult> GetWithCount(int PageNumber, int PageSize)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);

            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Booking Controller-GetWithCount");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var validFilter = new PaginationFilter(PageNumber, PageSize);

            BookingCountDTO data = new BookingCountDTO();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_bookings_with_count_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<BookingCountDTO>(cachedDataString);
                return Ok(new BaseResponse<BookingCountDTO>(data, true)); // IsCached = true
            }
            else
            {
                BaseResponse<BookingCountDTO> baseResponse = await Mediator.Send(new GetBookingWithCountQuery { PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize });

                if (baseResponse.Success)
                {
                    // loading from code (in real-time from database)
                    // then saving to the redis cache 
                    data = baseResponse.Data;
                    cachedDataString = JsonSerializer.Serialize<BookingCountDTO>(baseResponse.Data);
                    var expiryOptions = new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                        SlidingExpiration = TimeSpan.FromSeconds(300)
                    };
                    await DistributedCache.SetStringAsync("_bookings_with_count_" + validFilter.PageNumber, cachedDataString);
                    return Ok(baseResponse); // IsCached = false
                }
                Log.Error("401 Error. Unauthorized in Booking Controller: GetWithCount()");
                return StatusCode(401, new { Error = "Invalid token." });
            }
        }

        /// <summary>
        /// Get bookings of a particular student
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Booking to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // GET api/bookings/5/page/5/size/10
        [HttpGet("student/{id}/page/{PageNumber}/size/{PageSize}")]
        public async Task<IActionResult> GetBookingByStudent(int id, int PageNumber, int PageSize)
        {

            /**  Request.Headers.TryGetValue("Authorization", out var token);
              string role = await AuthHelper.GetRoleFromTokenAsync(token);
              Console.WriteLine("role", role);
              if (role != "student" || role != "admin")
                  return StatusCode(401, new { Error = "Unauthorized" });*/

            var validFilter = new PaginationFilter(PageNumber, PageSize);

            IEnumerable<Booking> data = new List<Booking>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_bookings_by_students_" + id + "_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<Booking>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<Booking>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetBookingByStudentQuery { Id = id, PageNumber = validFilter.PageNumber, PageSize = validFilter.PageSize });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                if (baseResponse.Success)
                {
                    data = baseResponse.Data;
                    cachedDataString = JsonSerializer.Serialize<IEnumerable<Booking>>(baseResponse.Data);
                    var expiryOptions = new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                        SlidingExpiration = TimeSpan.FromSeconds(300)
                    };
                    await DistributedCache.SetStringAsync("_bookings_by_students_" + id + "_" + validFilter.PageNumber, cachedDataString);
                    return Ok(baseResponse); // IsCached = false
                }
                Log.Error($"{baseResponse.Error.StatusCode} Error. {baseResponse.Error} in Booking Controller: GetBookingByStudent()");
                return StatusCode(baseResponse.Error.StatusCode, baseResponse.Error);
            }
        }

        /// <summary>
        /// Get bookings of a particular student
        /// </summary>
        // GET api/bookings/search/course/bachata/5/page/5/size/10
        [HttpGet("search/course/{Query}/page/{PageNumber}/size/{PageSize}")]
        public async Task<IActionResult> SearchBookingByCourseName(string Query)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "student" || role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Bookinging Controller: SearchBookingByCourseName");
                return StatusCode(401, new { Error = "Unauthorized" });
            }

            IEnumerable<Booking> data = new List<Booking>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_bookings_by_course_" + Query);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<Booking>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<Booking>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetBookingByCourseQuery { Query = Query });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<Booking>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_bookings_by_course_" + Query, cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Get bookings of a particular student
        /// </summary>
        // GET api/bookings/search/course/bachata/5/page/5/size/10
        [HttpPost("search/date/")]
        public async Task<IActionResult> SearchBookingByDateRange([FromBody] string StartDate, string EndDate, int PageNumber, int PageSize)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "student" || role != "admin")
                return StatusCode(401, new { Error = "Unauthorized" });

            var validFilter = new PaginationFilter(PageNumber, PageSize);

            IEnumerable<Booking> data = new List<Booking>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_bookings_by_date_from_" + StartDate + "_to_" + EndDate + "_" + validFilter.PageNumber);
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<Booking>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<Booking>>(data, true)); // IsCached = true
            }
            else
            {
                var baseResponse = await Mediator.Send(new GetBookingByDateRangeQuery
                {
                    StartDate = DateTime.Parse(StartDate),
                    EndDate = DateTime.Parse(EndDate),
                    PageNumber = validFilter.PageNumber,
                    PageSize = validFilter.PageSize
                });
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<Booking>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_bookings_by_date_from_" + StartDate + "_to_" + EndDate + "_" + validFilter.PageNumber, cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Create a new booking
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="500">If the database update failed</response>
        // POST api/bookings
        [HttpPost]
        public async Task<IActionResult> BookClass(BookClassCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin" || role != "student")
            {
                Log.Error($"401 Error. Unauthorized in Booking Controller: BookClass()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception e)
            {
                Log.Error($"Database create Error. {e} in Booking Controller");
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Delete a booking
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid or the Firebase id token is empty</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Booking to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/bookings/5/student/6
        [HttpDelete("{id}/student/{email}")]
        public async Task<IActionResult> Delete(int id, string email)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);

            if (role != "admin" || role != "student")
            {
                Log.Error("401 Error. Unauthorized in Booking Controller: Delete()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }

            if (role == "student")
            {
                string bookingOwner = await Mediator.Send(new GetStudentEmailByBookingIdQuery { Id = id });
                if (bookingOwner != email)
                {
                    Log.Error("401 Error. Unauthorized in Booking Controller: Delete()");
                    return StatusCode(401, new { Error = "Unauthorized" });
                }
            }
            try
            {
                return Ok(await Mediator.Send(new CancelBookingCommand { Id = id }));
            }
            catch (Exception e)
            {
                Log.Error($"Database delete Error. {e} in Booking Controller");
                return StatusCode(500, e);
            }
        }
    }
}
