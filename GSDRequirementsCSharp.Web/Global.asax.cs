using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using SimpleInjector;
using GSDRequirementsCSharp.Web.DependencyInjection;
using GSDRequirementsCSharp.Web.App_Start;
using Newtonsoft.Json;

namespace GSDRequirementsCSharp.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration
                               .Formatters
                               .JsonFormatter
                               .SerializerSettings
                               .Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            var apiContainer = new Container();
            apiContainer.ConfigureApi();
            var mvcContainer = new Container();
            mvcContainer.ConfigureMvc();
        }
    }
}