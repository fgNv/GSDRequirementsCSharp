using GSDRequirementsCSharp.Domain.Commands.Auditings;
using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Decorators
{
    class AuditingDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly ICommandHandler<AddAuditingCommand> _addAuditingCommandHandler;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public AuditingDecorator(ICommandHandler<T> decorated,
             ICurrentProjectContextId currentProjectContextId,
             ICurrentUserRetriever<User> currentUserRetriever,
                                 ICommandHandler<AddAuditingCommand> addAuditingCommandHandler)
        {
            _decorated = decorated;
            _currentProjectContextId = currentProjectContextId;
            _currentUserRetriever = currentUserRetriever;
            _addAuditingCommandHandler = addAuditingCommandHandler;
        }

        public void Handle(T command)
        {
            var commandDescriptionAttribute =
                Attribute.GetCustomAttribute(command.GetType(), typeof(CommandDescriptionAttribute)) as CommandDescriptionAttribute;
            if (commandDescriptionAttribute == null)
            {
                _decorated.Handle(command);
                return;
            }

            var projectId = _currentProjectContextId.Get();
            if (!projectId.HasValue)
            {
                _decorated.Handle(command);
                return;
            }

            var user = _currentUserRetriever.Get();
            if (user == null)
            {
                _decorated.Handle(command);
                return;
            }

            _decorated.Handle(command);

            var auditingCommand = new AddAuditingCommand();
            auditingCommand.ProjectId = projectId.Value;
            auditingCommand.UserId = user.Id;
            auditingCommand.Description = commandDescriptionAttribute.Description;

            _addAuditingCommandHandler.Handle(auditingCommand);
        }
    }
}
