using GSDRequirementsCSharp.Domain.Authentication;
using GSDRequirementsCSharp.Domain.Commands.Auditings;
using GSDRequirementsCSharp.Domain.Decorators;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Permissions;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Converter;
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
                                         new[] { typeof(ContainerExtensions).Assembly }, lifestyle);
            container.Register(typeof(IQueryHandler<,>),
                                         new[] { typeof(ContainerExtensions).Assembly }, lifestyle);
            container.Register(typeof(IConverter<,>),
                                         new[] { typeof(ContainerExtensions).Assembly }, lifestyle);

            container.Register<ICredentialsValidator, LocalCredentialsValidator>(lifestyle);

            container.RegisterDecorator(typeof(ICommandHandler<>),
                typeof(CommandHandlerPermissionDecorator<>),
                lifestyle);
            container.RegisterDecorator(typeof(ICommandHandler<>),
                                        typeof(AuditingDecorator<>),
                                        lifestyle,
                                        p => p.ServiceType != typeof(ICommandHandler<AddAuditingCommand>));
        }
    }
}
