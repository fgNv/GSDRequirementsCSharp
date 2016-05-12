using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.DataHydrators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.ViewModels.UseCases;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams
{
    class UseCaseDiagramsPaginatedQueryHandler : IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IssueHydration _issueHydration;

        public UseCaseDiagramsPaginatedQueryHandler(GSDRequirementsContext context,
            ICurrentProjectContextId currentProjectContextId,
            IssueHydration issueHydration)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
            _issueHydration = issueHydration;
        }

        public UseCaseDiagramsPaginatedQueryResult Handle(UseCaseDiagramsPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentProjectId = _currentProjectContextId.Get();

            var useCaseDiagramsQuery = _context.UseCaseDiagrams
                                             .Where(cd => cd.Project.Id == currentProjectId &&
                                                          cd.SpecificationItem.Active && cd.IsLastVersion);

            var maxPages = (int)Math.Ceiling(useCaseDiagramsQuery.Count() / (double)query.PageSize);

            var useCaseDiagrams = useCaseDiagramsQuery.OrderBy(cd => cd.Identifier)
                                        .Include(cd => cd.Contents)
                                        .Include(cd => cd.SpecificationItem.Package)
                                        .Skip(skip)
                                        .Take(query.PageSize)
                                        .Select(UseCaseDiagramViewModel.FromModel)
                                        .ToList();

            _issueHydration.Hydrate(useCaseDiagrams);

            var result = new UseCaseDiagramsPaginatedQueryResult(useCaseDiagrams, maxPages);
            return result;
        }
    }
}
