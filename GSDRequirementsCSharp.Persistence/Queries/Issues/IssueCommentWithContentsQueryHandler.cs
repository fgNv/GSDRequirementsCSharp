using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Data.Entity;
using System.Linq;
using GSDRequirementsCSharp.Domain.Queries;

namespace GSDRequirementsCSharp.Persistence.Queries.Issues
{
    class IssueCommentWithContentsQueryHandler : IQueryHandler<IssueCommentWithContentsQuery, IssueComment>
    {
        private readonly GSDRequirementsContext _context;

        public IssueCommentWithContentsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IssueComment Handle(IssueCommentWithContentsQuery query)
        {
            return _context.IssueComments
                           .Include(i => i.Contents)
                           .FirstOrDefault(i => i.Id == query.IssueCommentId);
        }
    }
}
