using System;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace microservice.Formatters
{
    /// <summary>
    /// Writes an object in a FHIR xml format to the output stream.
    /// </summary>
    public class ConformanceXmlOutputFormatter : TextOutputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConformanceXmlOutputFormatter"/> class.
        /// </summary>
        public ConformanceXmlOutputFormatter() 
        {
            SupportedEncodings.Add(new UTF8Encoding());
            SupportedMediaTypes.Add("application/xml");
            SupportedMediaTypes.Add("application/xml+fhir");
        }

        /// <summary>
        /// Queries whether this <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can serialization object of the specified type.
        /// </summary>
        /// <param name="type">The type to serialize.</param>
        /// <returns>
        /// true if the <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can serialize the type; otherwise, false.
        /// </returns>
        protected override bool CanWriteType(Type type)
        {
            return type == typeof(Conformance);
        }

        /// <inheritdoc />
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<ConformanceXmlOutputFormatter>>();

            FhirXmlSerializer serializer = new FhirXmlSerializer();
            string xml = serializer.SerializeToString((Resource) context.Object);

            await httpContext.Response.WriteAsync(xml);
        }
    }
}