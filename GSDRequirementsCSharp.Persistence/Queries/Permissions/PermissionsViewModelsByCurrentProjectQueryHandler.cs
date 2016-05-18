using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.Queries;

namespace GSDRequirementsCSharp.Persistence.Queries.Permissions
{
    class PermissionsViewModelsByCurrentProjectQueryHandler : IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<PermissionViewModel>>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public PermissionsViewModelsByCurrentProjectQueryHandler(GSDRequirementsContext context,
                                                       ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public IEnumerable<PermissionViewModel> Handle(PermissionsByCurrentProjectQuery query)
        {
            var projectId = _currentProjectContextId.Get();
            var permissions = _context.Permissions
                                      .Include(p => p.User.Contact)
                                      .Where(p => p.Project.Id == projectId && 
                                                  p.Profile != Profile.ProjectOwner)
                                      .Select(PermissionViewModel.FromModel)
                                      .ToList();

            return permissions;
        }
    }
}
