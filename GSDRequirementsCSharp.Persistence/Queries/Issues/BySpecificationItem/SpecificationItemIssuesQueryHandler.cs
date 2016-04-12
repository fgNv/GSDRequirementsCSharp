using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.ViewModels;

namespace GSDRequirementsCSharp.Persistence.Queries.Issues.BySpecificationItem
{
    class SpecificationItemIssuesQueryHandler : IQueryHandler<SpecificationItemIssuesQuery, IEnumerable<IssueViewModel>>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemIssuesQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<IssueViewModel> Handle(SpecificationItemIssuesQuery query)
        {
            return _context.Issues
                           .Include(i => i.IssueComments)
                           .Include(i => i.Contents)
                           .Where(i => i.SpecificationItemId == query.SpeficiationItemId)
                           .OrderByDescending(i => i.LastModification)
                           .Select(IssueViewModel.FromModel)                           
                           .ToList();
        }
    }
}
