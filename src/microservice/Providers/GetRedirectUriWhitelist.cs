using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace microservice.Providers
{
    public static class GetRedirectUriWhitelist
    {
        // TODO Add custom code here to return the whitelist for your system
        public static bool IsUriInWhitelist(string uri, IConfiguration config)
        {
            if(string.IsNullOrEmpty(uri)) { return false; }

            Uri checkUri;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out checkUri)) { return false; }

            var redirectUriWhitelist = config.GetSection("RedirectUriWhitelist").GetChildren()
                                                                .ToDictionary(x => x.Key, x => x.Value);
            if (redirectUriWhitelist.ContainsValue(uri))
                return true;

            return false;
        }
    }
}