using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
                                   .Where(uc => uc.UseCaseDiagram.Id == query.DiagramId)
                                   .ToList();

            return useCases;
        }
    }
}
