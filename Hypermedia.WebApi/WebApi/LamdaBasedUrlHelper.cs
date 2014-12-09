using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Composable.Hypermedia.WebApi
{
    public static class LamdaBasedUrlHelper
    {
        public static Link<TResult> Link<TController, TResult>(this UrlHelper @this, Expression<Func<TController, TResult>> expression)
        {
            return new Link<TResult>(Url(@this, expression));
        }

        private static string Url(this UrlHelper @this, LambdaExpression controllerCall)
        {
            return @this.CreateLink(new RouteValues(controllerCall));            
        }

        private static string CreateLink(this UrlHelper @this, RouteValues routeValues)
        {
            var values = routeValues.Values.ToDictionary(pair => pair.Key, pair => pair.Value);
            if (routeValues.ControllerType.IsSubclassOf(typeof(ApiController)))
            {
                values.Remove("controller");
                values.Remove("action");
            }
            return @this.Link(routeValues.RouteName(), values);
        }

        private static string RouteName(this RouteValues @this)
        {
            var routeAttribute = @this.ControllerCall.Method.GetCustomAttributes(typeof(RouteAttribute), false).OfType<RouteAttribute>().ToList();
            if (routeAttribute.Any())
            {
                return routeAttribute.Single().Name;
            }
            return null;
        }
    }
}