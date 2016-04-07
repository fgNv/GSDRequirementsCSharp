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
using Newtonsoft.Json.Serialization;
using System.Data.Entity.Migrations;
using GSDRequirementsCSharp.Persistence.Migrations;

namespace GSDRequirementsCSharp.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration
                               .Formatters
                               .JsonFormatter
                               .SerializerSettings
                               .Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration
                               .Formatters
                               .JsonFormatter
                               .SerializerSettings
                               .ContractResolver = new CamelCasePropertyNamesContractResolver();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var apiContainer = new Container();
            apiContainer.ConfigureApi();
            var mvcContainer = new Container();
            mvcContainer.ConfigureMvc();

            MigrationsRunner.RunMigrations();
        }
    }
}