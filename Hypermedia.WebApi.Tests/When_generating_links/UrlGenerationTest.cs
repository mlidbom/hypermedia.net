using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Composable.Hypermedia.WebApi;
using FluentAssertions;

namespace Hypermedia.WebApi.Tests.When_generating_links
{
    public class UrlGenerationTest
    {
        private static UrlHelper helper;

        private static UrlHelper UrlHelper
        {
            get
            {
                if(helper == null)
                {
                    var httpConfiguration = new HttpConfiguration();
                    httpConfiguration.MapHttpAttributeRoutes(new AutoNamingRouteProvider());
                    httpConfiguration.EnsureInitialized();
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
                    request.SetConfiguration(httpConfiguration);
                    helper = new UrlHelper(request);
                }

                return helper;
            }
        }

        protected void AssertLamdaCreatesExpectedLink<TController, TResult>(Expression<Func<TController, TResult>> expression, string expectedLink)
        {
            UrlHelper
                .Link<TController, TResult>(expression)
                .Url
                .Should()
                .Be("http://localhost/" + expectedLink);
        }
    }
}