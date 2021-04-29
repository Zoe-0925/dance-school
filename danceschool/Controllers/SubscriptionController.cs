using System.Collections.Generic;
using System.Threading.Tasks;
using danceschool.Api;
using danceschool.Handlers;
using danceschool.Handlers.CommandHandlers;
using danceschool.Handlers.QueryHandlers;
using danceschool.Helpers;
using danceschool.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace danceschool.Controllers
{
    /// <summary>
    /// The controller for the Model "Subscription"
    /// </summary>
    [Route("api/subscription")]
    public class SubscriptionController : BaseController
    {
        /// <summary>
        /// Get all Subscriptions
        /// </summary>
        // GET: api/subscription/
        [HttpGet("page/{PageNumber}/size/{PageSize}")]
        public async Task<IActionResult> GetSubscription(int PageNumber, int PageSize)
        {

            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to find subscription. 401 Error. Unauthorized in Subscription Controller: GetSubscription");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(new GetSubscriptionQuery { PageNumber = PageNumber, PageSize = PageSize });

            if (!result.Success)
            {
                Log.Error($"{result.Error.StatusCode} Error. {result.Error} in Booking Controller: GetSubscription()");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully found subscription at page: {PageNumber}.");
            return Ok(result);
        }

        /// <summary>
        /// Get a student's subscriptions
        /// </summary>
        // GET: api/subscription/student/5/history/page/1/size/10,
        [HttpGet("page/student/{id}/history/page/{PageNumber}/size/{PageSize}/")]
        public async Task<IActionResult> GetStudentSubscription(int id, int PageNumber, int PageSize)
        {


            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin")
            {
                Log.Error("Failed to find subscription by student of id: {id} at page: {PageNumber}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            BaseResponse<IEnumerable<Subscription>> result = (BaseResponse<IEnumerable<Subscription>>)await Mediator.Send(new GetSubscriptionHistoryQuery { Id = id, PageNumber = PageNumber, PageSize = PageSize });

            if (!result.Success)
            {
                Log.Error($"{result.Error.StatusCode} Error. {result.Error} in Booking Controller: GetStudentSubscription()");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully found subscription by student of id: {id} at page: {PageNumber}.");
            return Ok(result);
        }

        /// <summary>
        /// Create a new Subscriptions
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="500">If the database update failed</response>
        // POST api/subscription
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin" || role != "student")
            {
                Log.Error("Failed to create the subscription. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            Log.Information($"Successfully created the subscription of id:{result.Data}.");
            return Ok(result);
        }

        /// <summary>
        /// Update a particular Subscriptions
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Subscription to be updated is not found</response>
        /// <response code="500">If the database update failed</response>
        // PUT api/subscription
        [HttpPut]
        public async Task<IActionResult> UpdateSubscription(UpdateSubscriptionCommand command)
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin" || role != "student")
            {
                Log.Error("Failed to update the subscription of id:{command.Id}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                Log.Error($"Failed to update the subscription of id:{command.Id}. {result.Error.StatusCode} Error. {result.Error}.");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully updated the subscription of id:{command.Id}.");
            return Ok(result);
        }

        /// <summary>
        /// Delete a particular Subscriptions
        /// </summary>
        /// <response code="400">Bad request. If the command parameter is invalid</response>
        /// <response code="401">If the Firebase Authentication or Authorization failed</response>
        /// <response code="404">If the Subscription to be deleted is not found</response>
        /// <response code="500">If the database update failed</response>
        // DELETE api/subscription/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            Request.Headers.TryGetValue("Authorization", out var token);

            string role = await AuthHelper.GetRoleFromTokenAsync(token);
            if (role != "admin" || role != "student")
            {
                Log.Error("Failed to delete the subscription of id: {id}. 401 Error. Unauthorized.");
                return StatusCode(401, new { Error = "Unauthorized" });
            }
            var result = await Mediator.Send(new DeleteSubscriptionCommand { Id = id });

            if (!result.Success)
            {
                Log.Error($"Failed to delete the subscription of id:{id}. {result.Error.StatusCode} Error. {result.Error}");
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            Log.Information($"Successfully deleted the subscription of id:{id}.");
            return Ok(result);
        }
    }
}
