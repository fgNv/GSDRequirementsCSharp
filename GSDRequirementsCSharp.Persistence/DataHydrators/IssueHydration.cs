using GSDRequirementsCSharp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.DataHydrators
{
    /// <summary>
    /// Service used to set the unclosed issues of a collection of specification items
    /// </summary>
    class IssueHydration
    {
        private readonly GSDRequirementsContext _context;

        public IssueHydration(GSDRequirementsContext context)
        {
            _context = context;
        }

        public void Hydrate(IEnumerable<IIssueable> issueables)
        {
            var itemsIds = issueables.Select(i => i.Id).ToList();
            var issues = _context.Issues
                                 .Include(i => i.Contents)
                                 .Include(i => i.Creator.Contact)
                                 .Include(i => i.IssueComments.Select(ic => ic.Contents))
                                 .Where(i => !i.Concluded && itemsIds.Contains(i.SpecificationItemId))
                                 .Select(IssueViewModel.FromModel)
                                 .ToList();

            foreach (var r in issueables)
                r.Issues = issues.Where(i => i.SpecificationItemId == r.Id);
        }
    }
}
