using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Composable.Hypermedia.WebApi
{
    public class RouteValues
    {
        private readonly LambdaExpression _lamba;
        private readonly MethodCallExpression _controllerCall;

        public static RouteValues Create<TController, TResponse>(Expression<Func<TController, TResponse>> controllerCall)
        {
            return new RouteValues(controllerCall);
        }

        public RouteValues(LambdaExpression controllerMethodCall)
        {
            _lamba = controllerMethodCall;

            _controllerCall = ExtractControllerCall();

            Controller = ExtractControllerName();
        }

        private string ExtractControllerName()
        {
            var controllerType = _controllerCall.Method.DeclaringType;
            var controller = controllerType.Name;
            return controller.ToLower().EndsWith("controller") ? controller.Substring(0, controller.Length - 10) : controller;
        }

        private MethodCallExpression ExtractControllerCall()
        {
            var methodCall = _lamba.Body as MethodCallExpression;
            if(methodCall == null)
            {
                throw new Exception("lambda must be a call to a controller method");
            }
            return methodCall;
        }


        public string Controller { get; private set; }

        public string Action { get { return _controllerCall.Method.Name; } }

        public IReadOnlyDictionary<string, object> Values { get; private set; }
    }
}
