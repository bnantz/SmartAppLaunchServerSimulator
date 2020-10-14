using System;
using microservice.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unittests
{
    [TestClass]
    public class TokenControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullLoggerCheck()
        {
            TokenController controller = new TokenController(null, new ConfigurationSection(null, ""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullConfigurationCheck()
        {
            TokenController controller = new TokenController(new NullLogger<TokenController>(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullLoggerConfigurationCheck()
        {
            TokenController controller = new TokenController(null, null);
        }
    }
}
