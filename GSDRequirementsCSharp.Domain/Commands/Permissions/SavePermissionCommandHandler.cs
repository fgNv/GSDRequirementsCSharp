using GSDRequirementsCSharp.Domain.Queries.Permissions;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
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
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public SavePermissionCommandHandler(IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>> permissionsByCurrentProjectQueryHandler,
                                            IRepository<Permission, Guid> permissionRepository,
                                            ICurrentProjectContextId currentProjectContextId)
        {
            _permissionsByCurrentProjectQueryHandler = permissionsByCurrentProjectQueryHandler;
            _permissionRepository = permissionRepository;
            _currentProjectContextId = currentProjectContextId;
        }

        public void Handle(SavePermissionCommand command)
        {
            var currentPermissions = _permissionsByCurrentProjectQueryHandler.Handle(null);

            foreach(var permission in currentPermissions)
            {
                var item = command.Items.FirstOrDefault(i => i.UserId == permission.UserId);
                if (item == null)
                {
                    _permissionRepository.Remove(permission);
                    continue;
                }
                permission.Profile = item.Profile;
            }

            var projectId = _currentProjectContextId.Get();
            //var project = _projectRepository.Get(projectId);
            foreach (var item in command.Items)
            {
                if (currentPermissions.Any(cp => cp.UserId == item.UserId))
                    continue;

                var permission = new Permission();
                permission.Id = Guid.NewGuid();
                permission.Profile = item.Profile;
                permission.ProjectId = projectId;
                permission.UserId = item.UserId;
                _permissionRepository.Add(permission);
            }
        }
    }
}
