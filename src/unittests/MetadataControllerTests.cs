using System;
using microservice.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unittests
{
    [TestClass]
    public class MetadataControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullCheck()
        {
            MetadataController controller = new MetadataController(null);
        }
    }
}
