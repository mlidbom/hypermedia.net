﻿using System.Web.Http;
using Composable.Hypermedia.WebApi;
using FluentAssertions;
using NUnit.Framework;

namespace Hypermedia.WebApi.Tests.When_extracting_route_values
{
    [TestFixture]
    public class From_method_Echo_with_single_string_parameter_text_with_argument_value_hello_coming_from_member_access
    {
        private RouteValues _values;

        private class ValueHolder
        {
            public string Value { get; set; }
        }

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
            var valueHolder = new ValueHolder() {Value = "hello"};
            _values = RouteValues.Create((MyEchoController controller) => controller.Echo(valueHolder.Value));
        }

        [Test]
        public void Route_value_text_is_hello()
        {
            _values.Values["text"].Should().Be("hello");
        }
    }
}