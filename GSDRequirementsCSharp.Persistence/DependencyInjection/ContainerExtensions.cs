using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Context;
using GSDRequirementsCSharp.Persistence.Repositories;
using SimpleInjector;

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
            
            container.Register(typeof(ICommandHandler<>), 
                                         new[] { typeof(ContainerExtensions).Assembly });
            container.Register(typeof(IQueryHandler<,>),
                                         new[] { typeof(ContainerExtensions).Assembly });
            container.Register(typeof(IRepository<,>),
                                         new[] { typeof(ContainerExtensions).Assembly });
            container.Register<GSDRequirementsContext, GSDRequirementsContext>(lifestyle);
            container.Register<IUserRepository<User>, AuthenticationUserRepository>(lifestyle);
        }
    }
}
