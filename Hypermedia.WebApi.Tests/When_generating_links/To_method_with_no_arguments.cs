using System;
using System.Web.Http;
using NUnit.Framework;
using LamdaBasedUrlHelper = Composable.Hypermedia.WebApi.LamdaBasedUrlHelper;

namespace Hypermedia.WebApi.Tests.When_generating_links
{
    [TestFixture]
    public class To_method_with_no_arguments : UrlGenerationTest
    {
        public class EchoController : ApiController
        {
            [Route("Hypermedia.WebApi.Tests.When_generating_links/index", Name = "46661ABC-8C41-4DDB-B54C-D2935B2C7808")]
            public string GetIndex()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void Link_is_correct()
        {
            AssertLamdaCreatesExpectedLink((EchoController controller) => controller.GetIndex(),
                "Hypermedia.WebApi.Tests.When_generating_links/index");                
        }
    }
}
