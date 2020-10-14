using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace microservice.Providers
{
    public static class GetValidClientIds
    {
        // TODO Add custom code here to return the valid client identifiers for your system
        public static bool IsValidClientId(string clientId, IConfiguration config)
        {
            if(string.IsNullOrEmpty(clientId)) { return false; }

            var redirectUriWhitelist = config.GetSection("ClientIdentifiers").GetChildren()
                                                                .ToDictionary(x => x.Key, x => x.Value);
            if (redirectUriWhitelist.ContainsValue(clientId.ToLowerInvariant()))
                return true;

            return false;
        }
    }
}