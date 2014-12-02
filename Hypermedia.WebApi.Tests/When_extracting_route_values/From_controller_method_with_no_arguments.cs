using System.Web.Http;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_extracting_route_values
{
    [TestFixture]
    public class From_method_call_to_method_sayHi_with_no_arguments_on_controller_myController
    {
        private RouteValues _values;

        public class MyController : ApiController
        {
            public string SayHi()
            {
                return "Hi";
            }
        }

        [SetUp]
        public void CreateValues()
        {
            _values = RouteValues.Create((MyController controller) => controller.SayHi());
        }

        [Test]
        public void Controller_is_my()
        {
            _values.Controller.Should().Be("My");
        }

        [Test]
        public void Action_is_SayHi()
        {
            _values.Action.Should().Be("SayHi");
        }
    }
}