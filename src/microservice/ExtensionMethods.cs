using Microsoft.Extensions.Configuration;

namespace microservice
{
    public static class ExtensionMethods
    {
        public static T Get<T>(this IConfiguration config, string key) where T : new()
        {
            var instance = new T();
            config.GetSection(key).Bind(instance);
            return instance;
        }
    }
}
