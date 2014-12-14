using System;
using System.Web.Http;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_generating_links
{
   
    [TestFixture]
    public class To_method_with_single_string_argument : UrlGenerationTest
    {
        private const string RouteTemplate = "AB59FD02-8874-4A0D-A29C-0988BEEE8992/echo/{text}";

        public class MethodWithSingleStringArgumentController : UrlGenerationTestController
        {
            [Route(RouteTemplate, Name = "AB59FD02-8874-4A0D-A29C-0988BEEE8992")]
            public string Echo(string text)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void Link_is_correct()
        {
            AssertLamdaCreatesExpectedLink(
                (MethodWithSingleStringArgumentController controller) => controller.Echo("hello"),
                RouteTemplate.Replace("{text}", "hello"));                
        }
    }
}

