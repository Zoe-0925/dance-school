using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace danceschool.Controllers
{
    /// <summary>
    /// The base controller that contains the Mediator
    /// </summary>
    [ApiController]
    [Route("api")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IDistributedCache _distributedCache;

        /// <summary>
        /// Using Mediator to route commands and queries
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IDistributedCache DistributedCache => _distributedCache ??= HttpContext.RequestServices.GetService<IDistributedCache>();

        [HttpGet]
        [Route("clear-cache")]
        public async Task<IActionResult> ClearCache(string key)
        {
            await _distributedCache.RemoveAsync(key);
            return Ok(new { Message = $"cleared cache for key -{key}" });
        }
    }
}