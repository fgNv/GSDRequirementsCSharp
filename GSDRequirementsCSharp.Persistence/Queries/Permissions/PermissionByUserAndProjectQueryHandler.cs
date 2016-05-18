using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;

namespace GSDRequirementsCSharp.Persistence.Queries.Permissions
{
    internal class PermissionByUserAndProjectQueryHandler : IQueryHandler<PermissionByUserAndProjectQuery, Permission>
    {
        private readonly GSDRequirementsContext _context;

        public PermissionByUserAndProjectQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Permission Handle(PermissionByUserAndProjectQuery query)
        {
            return _context.Permissions
                           .FirstOrDefault(p => p.ProjectId == query.ProjectId && 
                                                p.UserId == query.UserId);
        }
    }
}
