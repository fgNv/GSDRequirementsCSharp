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
using GSDRequirementsCSharp.Domain.Queries;

namespace GSDRequirementsCSharp.Persistence.Queries.Auditings
{
    class AuditingsByPeriodQueryHandler : IQueryHandler<AuditingsByPeriodQuery, IEnumerable<AuditingViewModel>>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public AuditingsByPeriodQueryHandler(GSDRequirementsContext context,
                                             ICurrentUserRetriever<User> currentUserRetriever)
        {
            _context = context;
            _currentUserRetriever = currentUserRetriever;
        }

        private DateTime? GetDateTimeFilterFromPeriod(AuditingPeriod period)
        {
            switch (period)
            {
                case AuditingPeriod.AllTime:
                    return null;
                case AuditingPeriod.LastMonth:
                    return DateTime.Now.AddMonths(-1);
                case AuditingPeriod.LastWeek:
                    return DateTime.Now.AddDays(-7);
                case AuditingPeriod.LastYear:
                    return DateTime.Now.AddYears(-1);
                default:
                    throw new Exception(Sentences.auditingPeriodNotDefined);
            }
        }

        public IEnumerable<AuditingViewModel> Handle(AuditingsByPeriodQuery query)
        {
            var currentUser = _currentUserRetriever.Get();            
            var projectsIds = _context.Permissions
                                   .Where(p => p.UserId == currentUser.Id)
                                   .Select(p => p.ProjectId)
                                   .Distinct()
                                   .ToList();                        

            var dateTimeFilter = GetDateTimeFilterFromPeriod(query.Period);
            var auditings = _context.Auditings
                                    .Include(a => a.Project.ProjectContents)
                                    .Include(a => a.User.Contact)
                                    .Where(a => projectsIds.Contains(a.ProjectId) &&
                                                (!dateTimeFilter.HasValue || a.ExecutedAt >= dateTimeFilter.Value))
                                    .OrderByDescending(a => a.ExecutedAt)
                                    .Select(AuditingViewModel.FromModel)
                                    .ToList();
            return auditings;
        }
    }
}
