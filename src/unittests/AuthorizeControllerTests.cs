using System;
using microservice.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unittests
{
    [TestClass]
    public class AuthorizeControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullLoggerCheck()
        {
            AuthorizeController controller = new AuthorizeController(null, new ConfigurationSection(null,""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullConfigurationCheck()
        {
            AuthorizeController controller = new AuthorizeController(new NullLogger<AuthorizeController>(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullLoggerConfigurationCheck()
        {
            AuthorizeController controller = new AuthorizeController(null, null);
        }
    }
}
