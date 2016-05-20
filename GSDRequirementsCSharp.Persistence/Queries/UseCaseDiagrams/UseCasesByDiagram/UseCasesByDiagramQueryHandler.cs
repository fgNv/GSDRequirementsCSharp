using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.Queries;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.UseCasesByDiagram
{
    class UseCasesByDiagramQueryHandler : IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>>
    {
        private readonly GSDRequirementsContext _context;

        public UseCasesByDiagramQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<UseCase> Handle(UseCasesByDiagramQuery query)
        {
            var useCases = _context.UseCases
                                   .Include(uc => uc.SpecificationItem)
                                   .Include(uc => uc.Contents)
                                   .Include(uc => uc.PreConditions.Select(pc => pc.Contents))
                                   .Include(uc => uc.PostConditions.Select(pc => pc.Contents))
                                   .Where(uc => uc.UseCaseDiagram.Id == query.DiagramId &&
                                                (!query.Version.HasValue || 
                                                  uc.Version == query.Version.Value))
                                   .ToList();
            return useCases;
        }
    }
}
