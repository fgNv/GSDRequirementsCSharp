using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Auditings
{
    class AuditingsByProjectQueryHandler : IQueryHandler<AuditingsByProjectQuery, IEnumerable<AuditingViewModel>>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public AuditingsByProjectQueryHandler(GSDRequirementsContext context,
                                             ICurrentUserRetriever<User> currentUserRetriever)
        {
            _context = context;
            _currentUserRetriever = currentUserRetriever;
        }

        public IEnumerable<AuditingViewModel> Handle(AuditingsByProjectQuery query)
        {
            var currentUser = _currentUserRetriever.Get();
            var hasPermission = _context.Permissions
                                        .Include(p => p.Project)
                                        .Any(p => p.UserId == currentUser.Id && p.ProjectId == query.ProjectId ||
                                                  p.Project.OwnerId == currentUser.Id);

            if (!hasPermission)
                throw new Exception(Sentences.youDontHavePermissionToAccessThisProject);

            var auditingsQuery = _context.Auditings
                                    .Include(a => a.Project.ProjectContents)
                                    .Include(a => a.User.Contact)
                                    .Where(a => a.ProjectId == query.ProjectId)
                                    .OrderByDescending(a => a.ExecutedAt)
                                    .Select(AuditingViewModel.FromModel);

            var auditings = auditingsQuery.ToList();
            return auditings;
        }
    }
}
