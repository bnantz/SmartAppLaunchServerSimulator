using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace microservice.Providers
{
    public static class SaveCode
    {
        private const string CodeKey = "code";

        // TODO Add custom code here to save the code to your system
        public static void Save(string code, IMemoryCache cache)
        {
            if (code == null) { throw new ArgumentNullException(nameof(code)); }
            if (cache == null) { throw new ArgumentNullException(nameof(cache)); }

            List<string> codes = (List<string>)cache.Get(CodeKey) ?? new List<string>();
            if (!codes.Contains(code))
            {
                codes.Add(code);
                cache.Set(CodeKey, codes);
            }
        }
    }
}