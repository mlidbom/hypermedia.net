using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_generating_links
{
    [TestFixture]
    public class To_method_with_no_arguments
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

        private void AssertLamdaCreatesExpectedLink<TController, TResult>(Expression<Func<TController, TResult>> expression, string expectedLink)
        {
            UrlHelper
                .Link(expression)
                .Url
                .Should()
                .Be("http://localhost/" + expectedLink);
        }

        private static UrlHelper helper;

        private static UrlHelper UrlHelper
        {
            get
            {
                if(helper == null)
                {
                    var httpConfiguration = new HttpConfiguration();
                    httpConfiguration.MapHttpAttributeRoutes();
                    httpConfiguration.EnsureInitialized();
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
                    request.SetConfiguration(httpConfiguration);
                    helper = new UrlHelper(request);
                }

                return helper;
            }
        }
    }
}
