using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.WithClassDiagrams
{
    class SpecificationItemWithClassDiagramsQueryHandler : IQueryHandler<SpecificationItemWithClassDiagramsQuery, SpecificationItem>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemWithClassDiagramsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public SpecificationItem Handle(SpecificationItemWithClassDiagramsQuery query)
        {
            return _context.SpecificationItems
                           .Include(si => si.ClassDiagrams)
                           .FirstOrDefault(si => si.Id == query.Id);
        }
    }
}
