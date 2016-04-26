using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.ViewModels;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Paginated
{
    class ClassDiagramsPaginatedQueryHandler : IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public ClassDiagramsPaginatedQueryHandler(GSDRequirementsContext context,
                                                  ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public ClassDiagramsPaginatedQueryResult Handle(ClassDiagramsPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentProjectId = _currentProjectContextId.Get();

            var classDiagramsQuery = _context.ClassDiagrams
                                             .Where(cd => cd.Project.Id == currentProjectId && 
                                                          cd.Active);

            var maxPages = (int)Math.Ceiling(classDiagramsQuery.Count() / (double)query.PageSize);

            var classDiagrams = classDiagramsQuery.OrderBy(p => p.Identifier)
                                        .Include(p => p.Contents)
                                        .Skip(skip)
                                        .Take(query.PageSize)
                                        .Select(ClassDiagramViewModel.FromModel)
                                        .ToList();

            var result = new ClassDiagramsPaginatedQueryResult(classDiagrams, maxPages);
            return result;
        }
    }
}
