using System;
using System.Web.Http;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_extracting_route_values
{
    public class From_call_with_single_nullable_int_named_argument_with_value_1
    {
        private RouteValues _values;
        private int Argument1 = 1;

        public class MyController : ApiController
        {
            public string Echo(int? argument)
            {
                throw new NotImplementedException();
            }
        }

        [SetUp]
        public void CreateValues()
        {
            _values = RouteValues.Create((MyController controller) => controller.Echo(Argument1));
        }


        [Test]
        public void Argument_value_should_be_1()
        {
            _values.Values["argument"].Should().Be(1);
        }
    }
}