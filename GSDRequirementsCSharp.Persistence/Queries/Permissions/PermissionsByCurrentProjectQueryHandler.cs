﻿using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Collections.Generic;
using System.Linq;

namespace GSDRequirementsCSharp.Persistence.Queries.Permissions
{
    class PermissionsByCurrentProjectQueryHandler : IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public PermissionsByCurrentProjectQueryHandler(GSDRequirementsContext context,
                                                       ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public IEnumerable<Permission> Handle(PermissionsByCurrentProjectQuery query)
        {
            var projectId = _currentProjectContextId.Get();
            var permissions = _context.Permissions
                                      .Where(p => p.Project.Id == projectId && 
                                                  p.Profile != Profile.ProjectOwner)
                                      .ToList();

            return permissions;
        }
    }
}
