using GSDRequirementsCSharp.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new CurrentCultureSetterApiFilter());
            config.Filters.Add(new HandleApiException());
        }
    }
}
