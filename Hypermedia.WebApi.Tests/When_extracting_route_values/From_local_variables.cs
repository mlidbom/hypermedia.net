using System.Web.Http;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_extracting_route_values
{
    public class From_local_variables_local2_string_1_and_local2_int_1
    {
        private RouteValues _values;

        public class MyController : ApiController
        {
            public string Echo2(string argument1, int argument2)
            {
                return "Hi";
            }
        }

        [SetUp]
        public void CreateValues()
        {
            var local = "1";
            var local2 = 2;
            _values = RouteValues.Create((MyController controller) => controller.Echo2(local, local2));
        }

        [Test]
        public void Local2_should_be_string_1()
        {
            _values.Values["argument1"].Should().Be("1");
        }

        [Test]
        public void Local2_should_be_integer_2()
        {
            _values.Values["argument2"].Should().Be(2);
        }
    }
}