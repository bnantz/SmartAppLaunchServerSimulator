using System;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RA;

namespace bddtests
{
    [TestClass]
    public class OAuth2AuthorizeTests
    {
        [TestMethod]
        public void POST_Success()
        {
            string body = "scope=launch&response_type=code&client_id=12345&launch=random&aud=audience&state=random&redirect_uri=https://localhost:44386/metadata";

            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/authorize")
                .Then()
                .TestStatus("is-ok", x => x == StatusCodes.Status200OK)
                    .Assert("is-ok")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_InvalidClientId()
        {
            string body = "scope=launch&response_type=code&client_id=badclientid&launch=random&aud=audience&state=random&redirect_uri=https://localhost:44386";

            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/authorize")
                .Then()
                .TestStatus("is-bad-request", x => x == StatusCodes.Status400BadRequest)
                .Assert("is-bad-request")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_BlankSate()
        {
            string body = "scope=launch&response_type=code&client_id=badclientid&launch=random&aud=audience&state=&redirect_uri=https://localhost:44386";

            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/authorize")
                .Then()
                .TestStatus("is-bad-request", x => x == StatusCodes.Status400BadRequest)
                .Assert("is-bad-request")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_BlankAudience()
        {
            string body = "scope=launch&response_type=code&client_id=badclientid&launch=random&aud=&state=random&redirect_uri=https://localhost:44386";

            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/authorize")
                .Then()
                .TestStatus("is-bad-request", x => x == StatusCodes.Status400BadRequest)
                .Assert("is-bad-request")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_BlankLaunch()
        {
            string body = "scope=launch&response_type=code&client_id=badclientid&launch=&aud=audience&state=random&redirect_uri=https://localhost:44386";

            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/authorize")
                .Then()
                .TestStatus("is-bad-request", x => x == StatusCodes.Status400BadRequest)
                .Assert("is-bad-request")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_InvalidUri()
        {
            string body = "scope=launch&response_type=code&client_id=123455&launch=random&aud=audience&state=random&redirect_uri=notauri";

            new RestAssured()
                .Given()
                .Name("InvalidUri Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/token")
                .Then()
                .TestStatus("is-bad-request", x => x == StatusCodes.Status400BadRequest)
                .Assert("is-bad-request")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_NonWhitelistUri()
        {
            string body = "scope=launch&response_type=code&client_id=123455&launch=random&aud=audience&state=random&redirect_uri=https://google.com";

            new RestAssured()
                .Given()
                .Name("NonWhitelistUri Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/token")
                .Then()
                .TestStatus("is-bad-request", x => x == StatusCodes.Status400BadRequest)
                .Assert("is-bad-request")
                .WriteAssertions()
                .Debug();
        }
    }
}
