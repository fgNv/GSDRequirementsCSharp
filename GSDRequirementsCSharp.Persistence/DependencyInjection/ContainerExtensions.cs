using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Context;
using SimpleInjector;
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
        }
    }
}
