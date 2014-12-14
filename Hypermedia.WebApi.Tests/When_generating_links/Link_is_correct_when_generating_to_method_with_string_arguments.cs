using System;
using System.Web.Http;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_generating_links
{
   
    [TestFixture]
    public class Link_is_correct_when_generating_to_method_with_string_arguments : UrlGenerationTest
    {
        private const string OneArgumentRouteTemplate = "AB59FD02-8874-4A0D-A29C-0988BEEE8992/echo/{text}";
        private const string TwoArgumentsRouteTemplate = "0CA44C8F-D5B1-41BC-9D0A-964CDB9443E4/twoargs/{arg1}/{arg2}";

        public class MethodWithSingleStringArgumentController : UrlGenerationTestController
        {
            [Route(OneArgumentRouteTemplate)]
            public string OneArgument(string text)
            {
                throw new NotImplementedException();
            }

            [Route(TwoArgumentsRouteTemplate)]
            public string TwoArguments(string arg1, string arg2)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void With_one_argument()
        {
            AssertLamdaCreatesExpectedLink(
                (MethodWithSingleStringArgumentController controller) => controller.OneArgument("hello"),
                OneArgumentRouteTemplate.Replace("{text}", "hello"));                
        }

        [Test]
        public void With_two_arguments()
        {
            AssertLamdaCreatesExpectedLink(
                (MethodWithSingleStringArgumentController controller) => controller.TwoArguments("val1", "val2"),
                TwoArgumentsRouteTemplate.Replace("{arg1}", "val1").Replace("{arg2}", "val2"));
        }
    }
}

