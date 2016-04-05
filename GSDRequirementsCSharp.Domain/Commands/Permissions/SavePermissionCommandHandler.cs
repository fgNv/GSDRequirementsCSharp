using GSDRequirementsCSharp.Domain.Queries.Permissions;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Permissions
{
    public class SavePermissionCommandHandler : ICommandHandler<SavePermissionCommand>
    {
        private readonly IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>> _permissionsByCurrentProjectQueryHandler;
        private readonly IRepository<Permission, Guid> _permissionRepository;

        public SavePermissionCommandHandler(IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>> permissionsByCurrentProjectQueryHandler,
                                            IRepository<Permission, Guid> permissionRepository)
        {
            _permissionsByCurrentProjectQueryHandler = permissionsByCurrentProjectQueryHandler;
            _permissionRepository = permissionRepository;
        }

        public void Handle(SavePermissionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
