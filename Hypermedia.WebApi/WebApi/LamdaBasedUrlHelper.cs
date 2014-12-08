using System;
using System.ComponentModel;
using System.Linq.Expressions;
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
            var routeValues = new RouteValues(controllerCall);
            return routeValues.CreateLink(@this);            
        }
    }
}