using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace microservice.Models
{
    public class AccessTokenRequest
    {
        [FromForm(Name = "grant_type")]
        public string GrantType { get; set; }

        [FromForm(Name = "code")]
        public string Code { get; set; }

        [FromForm(Name = "redirect_uri")]
        public string RedirectUrl { get; set; }

        [FromForm(Name = "state")]
        public string State { get; set; }

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