using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RA;

namespace bddtests
{
    [TestClass]
    public class OAuth2TokenTests
    {
        [TestMethod]
        public void POST_Success()
        {
            string body = "grant_type=authorization_code&code=123455&state=random&redirect_uri=https://localhost:44386/metadata";

            new RestAssured()
                .Given()
                .Name("Successful Call")
                .Header("Content-Type", "application/x-www-form-urlencoded")
                .Body(body)
                .When()
                .Post("https://localhost:44386/oauth2/token")
                .Then()
                .TestStatus("is-ok", x => x == StatusCodes.Status200OK)
                    .Assert("is-ok")
                .WriteAssertions()
                .Debug();
        }

        [TestMethod]
        public void POST_Failure_InvalidUri()
        {
            string body = "grant_type=authorization_code&code=123455&state=random&redirect_uri=notaurl";

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
            string body = "grant_type=authorization_code&code=123455&state=random&redirect_uri=https://google.com";

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
