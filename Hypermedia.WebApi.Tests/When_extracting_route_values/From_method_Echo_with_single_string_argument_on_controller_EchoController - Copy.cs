using System.Web.Http;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_extracting_route_values
{
    [TestFixture]
    public class From_method_Echo_with_single_string_parameter_text_with_argument_value_hello_on_controller_EchoController
    {
        private RouteValues _values;

        public class MyEchoController : ApiController
        {
            public string Echo(string text)
            {
                return text;
            }
        }

        [SetUp]
        public void CreateValues()
        {
            _values = RouteValues.Create((MyEchoController controller) => controller.Echo("hello"));
        }

        [Test]
        public void Controller_is_Echo()
        {
            _values.Controller.Should().Be("MyEcho");
        }

        [Test]
        public void Action_is_Echo()
        {
            _values.Action.Should().Be("Echo");
        }

        [Test]
        public void Route_value_text_is_hello()
        {
            _values.Values["text"].Should().Be("hello");
        }
    }
}