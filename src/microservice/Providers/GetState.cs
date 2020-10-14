using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace microservice.Providers
{
    public static class GetState
    {
        private const string StateKey = "state";

        // TODO Add custom code here to get the state from your system
        public static bool IsValidState(string state, IMemoryCache cache)
        {
            if (string.IsNullOrEmpty(state)) { return false; }
            if (cache == null) { throw new ArgumentNullException(nameof(cache)); }

            List<string> states = (List<string>)cache.Get(StateKey);
            if (states != null && states.Contains(state))
                return true;

            return false;
        }
    }
}