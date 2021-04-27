using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using danceschool.Helpers;
using danceschool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the Model "Membership"
    /// </summary>
    /// <response code="401">If the Firebase Authentication or Authorization failed</response>   
    [Route("api/memberships")]
    public class MembershipController : BaseController
    {
        /// <summary>
        /// Get all Memberships
        /// </summary>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        // GET: api/memberships/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Membership> data = new List<Membership>();
            string cachedDataString = string.Empty;
            cachedDataString = await DistributedCache.GetStringAsync("_membrships");
            if (!string.IsNullOrEmpty(cachedDataString))
            {
                // loaded data from the redis cache.
                data = JsonSerializer.Deserialize<IEnumerable<Membership>>(cachedDataString);
                return Ok(new BaseResponse<IEnumerable<Membership>>(data, true)); // IsCached = true
            }
            else
            {
                BaseResponse<IEnumerable<Membership>> baseResponse = await Mediator.Send(new GetMembershipQuery());
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                data = baseResponse.Data;
                cachedDataString = JsonSerializer.Serialize<IEnumerable<Membership>>(baseResponse.Data);
                var expiryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                    SlidingExpiration = TimeSpan.FromSeconds(300)
                };
                await DistributedCache.SetStringAsync("_memberships", cachedDataString);
                return Ok(baseResponse); // IsCached = false
            }
        }

        /// <summary>
        /// Create a new membership
        /// </summary>
        // POST api/memberships
        [HttpPost]
        public async Task<IActionResult> CreateMembership(CreateMembershipCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Membership Controller: UpdateMembership");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update a particular membership
        /// </summary>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Membership to be updated is not found</response>
        /// <response code="500">If the database update failed</response>
        // PUT api/memberships/
        [HttpPut]
        public async Task<IActionResult> UpdateMembership(UpdateMembershipCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Membership Controller: UpdateMembership");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            BaseResponse<int> result = (BaseResponse<int>)await Mediator.Send(command);

            if (!result.Success)
            {
                Log.Error($"{result.Error.StatusCode} Error. {result.Error} in Membership Controller: UpdateMembership()");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete a particular membership
        /// </summary>
        /// <response code="400">Bad request. If the query parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Membership to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/memberships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembership(int id)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("401 Error. Unauthorized in Membership Controller: DeleteMembership");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            BaseResponse<int> result = (BaseResponse<int>)await Mediator.Send(new DeleteMembershipCommand { Id = id });

            if (!result.Success)
            {
                Log.Error($"{result.Error.StatusCode} Error. {result.Error} in Membership Controller: DeleteMembership()");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            return Ok(result);
        }
    }
}
