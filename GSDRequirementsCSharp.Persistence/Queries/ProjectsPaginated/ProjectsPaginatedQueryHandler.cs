using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    class ProjectsPaginatedQueryHandler : IQueryHandler<ProjectsPaginatedQuery,
                                                        ProjectsPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public ProjectsPaginatedQueryHandler(GSDRequirementsContext context,
                                                   ICurrentUserRetriever<User> currentUserRetriever)
        {
            _context = context;
            _currentUserRetriever = currentUserRetriever;
        }

        public ProjectsPaginatedQueryResult Handle(ProjectsPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentUser = _currentUserRetriever.Get();
            var dbQuery = _context.Projects
                                  .Where(p => p.owner_id == currentUser.Id);

            var maxPages = (int)Math.Ceiling(dbQuery.Count() / (double)query.PageSize);
            var projects = dbQuery.OrderBy(p => p.name)
                                  .Include(p => p.ProjectContents)                                    
                                  .Skip(skip)
                                  .Take(query.PageSize)
                                  .ToList();
            return new ProjectsPaginatedQueryResult(projects, maxPages);
        }
    }
}
