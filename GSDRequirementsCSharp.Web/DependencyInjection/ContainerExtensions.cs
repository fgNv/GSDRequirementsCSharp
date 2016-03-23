using GSDRequirementsCSharp.Infrastructure.DependencyInjection;
using GSDRequirementsCSharp.Persistence.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void Configure(this Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.RegisterPersistenceDependencies(container.Options.DefaultScopedLifestyle);
            container.RegisterInfrastructureDependencies(container.Options.DefaultScopedLifestyle);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.DependencyResolver =
                    new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}