using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems
{
    class SpecificationItemWithUseCaseDiagramsQueryHandler : IQueryHandler<SpecificationItemWithUseCaseDiagramsQuery, SpecificationItem>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemWithUseCaseDiagramsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public SpecificationItem Handle(SpecificationItemWithUseCaseDiagramsQuery query)
        {
            return _context.SpecificationItems
                           .Include(si => si.UseCaseDiagrams)
                           .FirstOrDefault(si => si.Id == query.Id);
        }
    }
}
