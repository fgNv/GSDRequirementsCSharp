using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Persistence.DataHydrators;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Paginated
{
    class ClassDiagramsPaginatedQueryHandler : IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IssueHydration _issueHydration;

        public ClassDiagramsPaginatedQueryHandler(GSDRequirementsContext context,
                                                  ICurrentProjectContextId currentProjectContextId,
                                                  IssueHydration issueHydration)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
            _issueHydration = issueHydration;
        }

        public ClassDiagramsPaginatedQueryResult Handle(ClassDiagramsPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentProjectId = _currentProjectContextId.Get();

            var classDiagramsQuery = _context.ClassDiagrams
                                             .Where(cd => cd.Project.Id == currentProjectId && 
                                                          cd.SpecificationItem.Active && cd.IsLastVersion);

            var maxPages = (int)Math.Ceiling(classDiagramsQuery.Count() / (double)query.PageSize);

            var classDiagrams = classDiagramsQuery.OrderBy(cd => cd.Identifier)
                                        .Include(cd => cd.Contents)
                                        .Include(cd => cd.SpecificationItem.Package)
                                        .Where(cd => cd.SpecificationItem.Active)
                                        .Skip(skip)
                                        .Take(query.PageSize)
                                        .Select(ClassDiagramViewModel.FromModel)
                                        .ToList();

            _issueHydration.Hydrate(classDiagrams);
            
            var result = new ClassDiagramsPaginatedQueryResult(classDiagrams, maxPages);
            return result;
        }
    }
}
