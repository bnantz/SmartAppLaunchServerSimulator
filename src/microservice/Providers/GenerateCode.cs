using System;

namespace microservice.Providers
{
    public static class GenerateCode
    {
        // TODO Add custom code here to generate a valid code for your system
        public static string Create()
        {
            string code = Guid.NewGuid().ToString().Replace("-","");
            return code;
        }
    }
}