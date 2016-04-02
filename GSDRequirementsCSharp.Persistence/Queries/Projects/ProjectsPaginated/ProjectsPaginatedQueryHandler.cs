using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.Projects.ProjectsPaginated;
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
                                  .Where(p => p.OwnerId == currentUser.Id && p.Active);

            var maxPages = (int)Math.Ceiling(dbQuery.Count() / (double)query.PageSize);
            var projects = dbQuery.OrderBy(p => p.Id)
                                  .Include(p => p.ProjectContents)                                    
                                  .Skip(skip)
                                  .Take(query.PageSize)
                                  .ToList()
                                  .Select(p => new ProjectViewModel
                                  {
                                      Id = p.Id,
                                      Name = p.GetName(),
                                      IsCurrentUserOwner = p.OwnerId == currentUser.Id,
                                      CreatedAt = p.CreatedAt,
                                      ProjectContents = p.ProjectContents
                                  })
                                  .ToList();
            return new ProjectsPaginatedQueryResult(projects, maxPages);
        }
    }
}
