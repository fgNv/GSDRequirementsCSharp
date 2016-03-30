using GSDRequirementsCSharp.Domain.DependencyInjection;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.DependencyInjection;
using GSDRequirementsCSharp.Persistence;
using GSDRequirementsCSharp.Persistence.Authentication;
using GSDRequirementsCSharp.Persistence.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void ConfigureApi(this Container container)
        {
            var lifestyle = new WebApiRequestLifestyle();
            container.RegisterDependencies(lifestyle);            
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.DependencyResolver =
                    new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterDependencies(this Container container, Lifestyle lifestyle)
        {
            container.RegisterInfrastructureDependencies(lifestyle);
            container.RegisterDomainDependencies(lifestyle);
            container.RegisterPersistenceDependencies(lifestyle);
            container.Register<ICurrentUserRetriever<User>, CurrentUserRetriever>(lifestyle);
        }

        public static void ConfigureMvc(this Container container)
        {
            var lifestyle = new WebRequestLifestyle();
            container.RegisterDependencies(lifestyle);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());            
            container.RegisterMvcIntegratedFilterProvider();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}