using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Context;
using SimpleInjector;
using SimpleInjector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void RegisterPersistenceDependencies(this Container container,
                                                           Lifestyle lifestyle)
        {
            container.RegisterDecorator(typeof(ICommandHandler<>), 
                                        typeof(CommandHandlerSaveChangesDecorator<>), 
                                        lifestyle);

            //container.RegisterAllOpenGeneric(typeof(ICommandHandler<>),)

            container.RegisterCollection(typeof(ICommandHandler<>), 
                                         new[] { typeof(ContainerExtensions).Assembly });
            container.RegisterCollection(typeof(IRepository<,>),
                                         new[] { typeof(ContainerExtensions).Assembly });

        }
    }
}
