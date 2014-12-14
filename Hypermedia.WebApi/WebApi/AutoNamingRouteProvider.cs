using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Composable.Hypermedia.WebApi
{
    public class AutoNamingRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(
            HttpActionDescriptor actionDescriptor)
        {
            var actionRouteFactories = base.GetActionRouteFactories(actionDescriptor);

            actionRouteFactories
                .OfType<RouteAttribute>()
                .Where(routeAttr => string.IsNullOrWhiteSpace(routeAttr.Name))
                .ToList()
                .ForEach(routeAttributeWithoutName =>
                {
                    routeAttributeWithoutName.Name = AutoRouteNamingProvider.Name(actionDescriptor);
                });

            return actionRouteFactories;
        }
    }

    public static class AutoRouteNamingProvider
    {
        private const string Format = "{0}/{1}/{2}";

        public static string Name(RouteValues values)
        {
            return string.Format(Format,
                values.ControllerType.FullName,
                values.Action,
                ParameterDescriptions(values.ActionMethodInfo));
        }

        public static string Name(HttpActionDescriptor actionDescriptor)
        {
            return string.Format(Format,
                actionDescriptor.ControllerDescriptor.ControllerType.FullName,
                actionDescriptor.ActionName,
                ParameterDescriptions(actionDescriptor));
        }

        private static string ParameterDescriptions(MethodInfo action)
        {
            var description = new StringBuilder();
            foreach (var parameter in action.GetParameters())
            {
                description.AppendFormat("{0}:{1}/", parameter.ParameterType, parameter.Name);
            }
            return description.ToString();
        }

        private static string ParameterDescriptions(HttpActionDescriptor actionDescripton)
        {
            var description = new StringBuilder();
            foreach (var parameter in actionDescripton.GetParameters())
            {
                description.AppendFormat("{0}:{1}/", parameter.ParameterType, parameter.ParameterName);
            }
            return description.ToString();
        }
    }
}