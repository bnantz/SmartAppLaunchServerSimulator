using System;
using System.Reflection;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace microservice.Controllers
{
    [Produces("application/json", "application/xml")]
    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly ILogger<MetadataController> _logger;

        public MetadataController(ILogger<MetadataController> logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        [HttpGet]
        public Conformance Get()
        {
            _logger.LogDebug($"Entering {MethodBase.GetCurrentMethod()}");

            Conformance conformance;
            using (XmlReader xmlReader = XmlReader.Create("metadata.xml"))
            {
                FhirXmlParser fhirXmlParser = new FhirXmlParser();
                conformance = fhirXmlParser.Parse<Conformance>(xmlReader);
            }

            Uri uri = new Uri(this.Request.GetDisplayUrl());
            conformance.Id = "(01)00840682142809(10)MUSENXSP2";
            conformance.Version = "(01)00840682142809(10)MUSENXSP2";
            conformance.Url = uri.ToString();
            conformance.Date = $"{DateTime.UtcNow.ToString("s")}Z";

            Extension authorizeExtension = conformance.Rest[0].Security.Extension[0].Extension[0];
            authorizeExtension.Value = new FhirUri($"{uri.Scheme}://{uri.Authority}/oauth2/authorize");

            Extension tokenExtension = conformance.Rest[0].Security.Extension[0].Extension[1];
            tokenExtension.Value = new FhirUri($"{uri.Scheme}://{uri.Authority}/oauth2/token");

            _logger.LogInformation(conformance.ToString());

            _logger.LogDebug($"Leaving {MethodBase.GetCurrentMethod()}");

            return conformance;
        }
    }
}
