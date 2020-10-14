using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace microservice.Models
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires-in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("patient")]
        public string Patient { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        public override string ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties();
            StringBuilder result = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
                result.AppendFormat("{0}={1} ", propertyInfo.Name, propertyInfo.GetValue(this));

            return result.ToString();
        }
    }
}