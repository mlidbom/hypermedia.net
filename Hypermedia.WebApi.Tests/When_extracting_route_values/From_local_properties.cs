using System;
using System.Web.Http;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_extracting_route_values
{
    public class From_local_property_variables_local2_string_1_and_local2_int_1
    {
        private RouteValues _values;
        private string Argument1 { get; set; }
        private int Argument2 { get; set; }

        public class MyController : ApiController
        {
            public string Echo2(string argument1, int argument2)
            {
                throw new NotImplementedException();
            }
        }

        [SetUp]
        public void CreateValues()
        {
            Argument1 = "1";
            Argument2 = 2;
            _values = RouteValues.Create((MyController controller) => controller.Echo2(Argument1, Argument2));
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