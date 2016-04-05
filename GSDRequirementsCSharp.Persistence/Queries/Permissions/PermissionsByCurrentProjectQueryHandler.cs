using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.Permissions;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Permissions
{
    class PermissionsByCurrentProjectQueryHandler : IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>>
    {
        private readonly GSDRequirementsContext _context;

        public PermissionsByCurrentProjectQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<Permission> Handle(PermissionsByCurrentProjectQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
