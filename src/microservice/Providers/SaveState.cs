using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace microservice.Providers
{
    public static class SaveState
    {
        private const string StateKey = "state";

        // TODO Add custom code here to save the state to your system
        public static void Save(string state, IMemoryCache cache)
        {
            if (state == null) { throw new ArgumentNullException(nameof(state)); }
            if (cache == null) { throw new ArgumentNullException(nameof(cache)); }

            List<string> states = (List<string>)cache.Get(StateKey) ?? new List<string>();
            if (!states.Contains(state))
            {
                states.Add(state);
                cache.Set(StateKey, states);
            }
        }
    }
}