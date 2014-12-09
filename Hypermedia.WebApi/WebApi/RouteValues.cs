using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Routing;
using Composable.Hypermedia.Linq.Expressions;

namespace Composable.Hypermedia.WebApi
{
    public class RouteValues
    {
        private readonly LambdaExpression _lambda;
        private readonly MethodCallExpression _controllerCall;
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public static RouteValues Create<TController, TResponse>(Expression<Func<TController, TResponse>> controllerCall)
        {
            return new RouteValues(controllerCall);
        }

        public RouteValues(LambdaExpression controllerMethodCall)
        {
            _lambda = controllerMethodCall;

            _controllerCall = ExtractControllerCall();

            RouteName = GetRouteName();
            _values["controller"] = Controller = ExtractControllerName();
            _values["action"] = Action;

            foreach(var argument in ExtractArguments())
            {
                _values[argument.ParameterName] = argument.Argument.ExtractValue();
            }
        }

        private ArgumentSpecification[] ExtractArguments()
        {
            return _controllerCall.Arguments.Select(
                (argument, index) => new ArgumentSpecification
                          {
                              Index = index,
                              Argument = argument,
                              ParameterName = _controllerCall.Method.GetParameters()[index].Name
                          }).ToArray();
        }

        private string GetRouteName()
        {
            var routeAttribute = _controllerCall.Method.GetCustomAttributes(typeof(RouteAttribute), false).OfType<RouteAttribute>().ToList();
            if(routeAttribute.Any())
            {
                return routeAttribute.Single().Name;
            }
            return null;
        }

        private string ExtractControllerName()
        {
            ControllerType = _controllerCall.Method.DeclaringType;
            var controller = ControllerType.Name;
            return controller.ToLower().EndsWith("controller") ? controller.Substring(0, controller.Length - 10) : controller;
        }

        private MethodCallExpression ExtractControllerCall()
        {
            var methodCall = _lambda.Body as MethodCallExpression;
            if(methodCall == null)
            {
                throw new Exception("lambda must be a call to a controller method");
            }
            return methodCall;
        }


        public Type ControllerType { get; private set; }
        public string Controller { get; private set; }

        public string Action { get { return _controllerCall.Method.Name; } }

        public IReadOnlyDictionary<string, object> Values { get { return _values; } }
        public string RouteName { get; set; }

        internal class ArgumentSpecification
        {
            public int Index { get; set; }
            public Expression Argument { get; set; }
            public string ParameterName { get; set; }
        }

        public string CreateLink(UrlHelper @this)
        {
            var values = Values.ToDictionary(pair => pair.Key, pair => pair.Value);
            if(ControllerType.IsSubclassOf(typeof(ApiController)))
            {
                values.Remove("controller");
                values.Remove("action");
            }
            return @this.Link(RouteName, values);
        }
    }
}
