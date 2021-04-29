using System.Text.Json;
using System.Threading.Tasks;
using danceschool.Handlers.QueryHandlers;
using danceschool.Helpers;
using danceschool.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using danceschool.Models;
using System;
using System.Collections.Generic;
using Serilog;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the analytical dashboard
    /// </summary>
    /// <response code="401">If the Firebase Authentication or Authorization failed</response>     
    [Route("api/analytics")]
    public class AnalyticsController : BaseController
    {
        /// <summary>
        /// Get the top classes and top Dashboards for the dashboard
        /// </summary>
        /// <response code="400">If the Firebase id token is empty</response>   
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>   
        // GET: api/analytics/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Analytical Controller: Get()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            Log.Information("Getting dashboard for Admin.");
            Dashboard data = new Dashboard();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_dashboard");
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                Log.Information("Returned cached results.");
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<Dashboard>(cachedDataString);
                return Ok(new BaseResponse<Dashboard>(data, true)); // IsCached = true
            }
            else
            {
                Log.Information("Cached new results.");
                BaseResponse<Dashboard> baseResponse = await Mediator.Send(new GetDashboardQuery());
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<Dashboard>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_dashboard", cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Get the total count of bookings by year for the dashboard
        /// </summary>
        // GET: api/analytics/bookings/lastYear
        [HttpGet("bookings/count/year")]
        public async Task<IActionResult> GetBookingCountByYear()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Analytical Controller: Get()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }

            Log.Information("Getting Booking Count By Year for Admin.");
            IEnumerable<CountByDateNumber> data = new List<CountByDateNumber>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_count_by_year");
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                Log.Information("Returned cached results.");
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<CountByDateNumber>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<CountByDateNumber>>(data, true)); // IsCached = true
            }
            else
            {
                Log.Information("Cached new results.");
                var baseResponse = await Mediator.Send(new GetBookingCountByYearQuery());
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<CountByDateNumber>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_count_by_year", cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Get the total count of bookings by month for the dashboard
        /// </summary>
        /// <response code="400">If the query parameter is invalid or the Firebase id token is empty</response>   
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>   
        // GET: api/analytics/bookings/lastYear
        [HttpGet("bookings/count/{type}")]
        public async Task<IActionResult> GetBookingCountByDateNumber(string type)
        {
            if (type != "year" && type != "month")
            {
                Log.Error($"400 Error. Invalid parameter @type {type} in Analytical Controller: Get()");
                return StatusCode(400, new { Error = "Invalid type." });
            }

            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Analytical Controller: Get()");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            Log.Information("Getting Booking Count By Date for Admin");

            IEnumerable<CountByDateNumber> data = new List<CountByDateNumber>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_count_by_month");
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                Log.Information("Returned cached results.");
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<CountByDateNumber>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<CountByDateNumber>>(data, true)); // IsCached = true
            }
            else
            {
                Log.Information("Cached new results.");
                var baseResponse = await Mediator.Send(new GetBookingCountByDateNumberQuery());
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<CountByDateNumber>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_count_by_month", cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }
    }
}