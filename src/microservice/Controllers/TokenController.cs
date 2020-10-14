using System;
using System.Reflection;
using microservice.Models;
using microservice.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace microservice.Controllers
{
    [Consumes("application/x-www-form-urlencoded")]
    [Route("oauth2/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IConfiguration _config;

        public TokenController(ILogger<TokenController> logger, IConfiguration config)
        {
            if (logger == null) { throw new ArgumentNullException(nameof(logger)); }
            if (config == null) { throw new ArgumentNullException(nameof(config)); }

            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromForm] AccessTokenRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            _logger.LogDebug($"Entering {MethodBase.GetCurrentMethod()}");

            if (!GetRedirectUriWhitelist.IsUriInWhitelist(request.RedirectUrl, _config) ||
                string.Compare(request.GrantType, "authorization_code", StringComparison.OrdinalIgnoreCase) != 0 ||
                !GetState.IsValidState(request.State, HttpContext.RequestServices.GetService<IMemoryCache>()) ||
                !GetCode.IsValidCode(request.Code, HttpContext.RequestServices.GetService<IMemoryCache>()))
            {
                return BadRequest();
            }

            AccessToken token = _config.Get<AccessToken>("token");
            token.State = request.State;
            
            _logger.LogInformation($"{token}");
            
            _logger.LogDebug($"Leaving {MethodBase.GetCurrentMethod()}");

            return Ok(token);
        }
    }
}