using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Permissions
{
    public class ProjectCommandHandlerPermissionDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly Infrastructure.Authentication.ICurrentUserRetriever _currentUserRetriever;
        private readonly IRepository<Permission, Guid> 

        public ProjectCommandHandlerPermissionDecorator(ICommandHandler<T> decorated)
        {
            _decorated = decorated;
        }

        public void Handle(T command)
        {
            throw new NotImplementedException();
        }
    }
}
