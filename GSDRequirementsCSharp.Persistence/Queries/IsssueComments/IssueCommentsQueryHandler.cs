using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.IsssueComments
{
    class IssueCommentsQueryHandler : IQueryHandler<IssueCommentsQuery, IEnumerable<IssueCommentViewModel>>
    {
        private readonly GSDRequirementsContext _context;

        public IssueCommentsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<IssueCommentViewModel> Handle(IssueCommentsQuery query)
        {
            var comments = _context.IssueComments
                                   .Include(ic => ic.Contents)
                                   .Include(ic => ic.Creator)
                                   .Where(ic => ic.IssueId == query.IssueId)
                                   .OrderByDescending(c => c.LastModification)
                                   .ToList()
                                   .Select(IssueCommentViewModel.FromModel);
            return comments;
        }
    }
}
