using GSDRequirementsCSharp.Domain.Authentication;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using SimpleInjector;
using SimpleInjector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void RegisterDomainDependencies(this Container container,
                                                           Lifestyle lifestyle)
        {     
            container.Register(typeof(ICommandHandler<>), 
                                         new[] { typeof(ContainerExtensions).Assembly });
            container.Register(typeof(IQueryHandler<,>),
                                         new[] { typeof(ContainerExtensions).Assembly });
            container.Register<ICredentialsValidator, LocalCredentialsValidator>(lifestyle);

        }
    }
}
