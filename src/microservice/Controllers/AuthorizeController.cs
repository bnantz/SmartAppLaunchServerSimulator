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
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<AuthorizeController> _logger;
        private readonly IConfiguration _config;

        public AuthorizeController(ILogger<AuthorizeController> logger, IConfiguration config)
        {
            if (logger == null)  { throw new ArgumentNullException(nameof(logger));  }
            if (config == null) { throw new ArgumentNullException(nameof(config)); }

            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromForm] AuthorizeRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            _logger.LogDebug($"Entering {MethodBase.GetCurrentMethod()}");

            if (!GetRedirectUriWhitelist.IsUriInWhitelist(request.RedirectUrl, _config) ||
                string.Compare(request.Scope, "launch", StringComparison.OrdinalIgnoreCase) != 0 ||
                string.Compare(request.ResponseType, "code", StringComparison.OrdinalIgnoreCase) != 0 ||
                !GetValidClientIds.IsValidClientId(request.ClientId, _config) ||
                string.IsNullOrEmpty(request.Launch) ||
                string.IsNullOrEmpty(request.Audience) ||
                string.IsNullOrEmpty(request.State))
            {
                return BadRequest();
            }

            SaveState.Save(request.State, HttpContext.RequestServices.GetService<IMemoryCache>());

            string code = GenerateCode.Create();
            SaveCode.Save(code, HttpContext.RequestServices.GetService<IMemoryCache>());

            string redirectUri = $"{request.RedirectUrl}?state={request.State}&code={code}";

            _logger.LogInformation($"Redirecting to {redirectUri}");

            _logger.LogDebug($"Leaving {MethodBase.GetCurrentMethod()}");

            return Redirect(redirectUri);
        }
    }
}
