using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RA;

namespace bddtests
{
    [TestClass]
    public class MetadataTests
    {
        [TestMethod]
        public void Get_XML_Success()
        {
            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/xml")
                .Header("Accept", "application/xml")
                .When()
                .Get("https://localhost:44386/metadata")
                .Then()
                .TestStatus("is-ok", x => x == StatusCodes.Status200OK)
                    .Assert("is-ok")
                .TestHeader("is-xml", "Content-Type", s => s.Equals("application/xml; charset=utf-8"))
                    .Assert("is-xml")
                .WriteAssertions()
                .Debug();
        }


        [TestMethod]
        public void Get_JSON_Success()
        {
            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/json")
                .Header("Accept", "application/json")
                .When()
                .Get("https://localhost:44386/metadata")
                .Then()
                .TestStatus("is-ok", x => x == StatusCodes.Status200OK)
                    .Assert("is-ok")
                .TestHeader("is-json", "Content-Type", s => s.Equals("application/json; charset=utf-8"))
                    .Assert("is-json")
                .WriteAssertions()
                .Debug();
        }
    }
}
