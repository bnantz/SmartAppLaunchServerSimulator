using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace microservice.Models
{
    public class AuthorizeRequest
    {
        [FromForm(Name = "scope")]
        public string Scope { get; set; }

        [FromForm(Name = "response_type")]
        public string ResponseType { get; set; }

        [FromForm(Name = "client_id")]
        public string ClientId { get; set; }

        [FromForm(Name = "launch")]
        public string Launch { get; set; }

        [FromForm(Name = "aud")]
        public string Audience { get; set; }

        [FromForm(Name = "state")]
        public string State { get; set; }

        [FromForm(Name = "redirect_uri")]
        public string RedirectUrl { get; set; }

        public override string ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties();
            StringBuilder result = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                result.AppendFormat("{0}={1} ", propertyInfo.Name, propertyInfo.GetValue(this));
            }

            return result.ToString();
        }
    }
}