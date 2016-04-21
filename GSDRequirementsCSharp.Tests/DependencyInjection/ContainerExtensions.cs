using GSDRequirementsCSharp.Infrastructure.DependencyInjection;
using GSDRequirementsCSharp.Persistence.DependencyInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Tests.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static Container BuildContainer()
        {
            var container = new Container();
            container.RegisterInfrastructureDependencies(Lifestyle.Transient);
            container.RegisterPersistenceDependencies(Lifestyle.Transient);

            return container;
        }

        public static T GetInstance<T>() where T : class
        {
            var container = BuildContainer();
            return container.GetInstance<T>();
        }
    }
}
