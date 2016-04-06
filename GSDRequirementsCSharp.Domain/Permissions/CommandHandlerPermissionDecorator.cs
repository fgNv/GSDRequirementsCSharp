using GSDRequirementsCSharp.Infrastructure;
using System;

namespace GSDRequirementsCSharp.Domain.Permissions
{
    public class CommandHandlerPermissionDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerPermissionDecorator(ICommandHandler<T> decorated, 
                                                 IServiceProvider serviceProvider)
        {
            _decorated = decorated;
            _serviceProvider = serviceProvider;
        }

        public void Handle(T command)
        {
            ICommandExtensions.VerifyPermission(command as dynamic, _serviceProvider);
            _decorated.Handle(command);
        }
    }
}
