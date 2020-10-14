using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace microservice.Providers
{
    public static class GetCode
    {
        private const string CodeKey = "code";

        // TODO Add custom code here to get the state from your system
        public static bool IsValidCode(string code, IMemoryCache cache)
        {
            if (string.IsNullOrEmpty(code)) { return false; }
            if (cache == null) { throw new ArgumentNullException(nameof(cache)); }

            List<string> codes = (List<string>)cache.Get(CodeKey);
            if (codes != null && codes.Contains(code))
                return true;

            return false;
        }
    }
}