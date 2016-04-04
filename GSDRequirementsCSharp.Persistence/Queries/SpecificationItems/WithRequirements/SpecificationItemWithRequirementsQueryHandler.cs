using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.WithRequirements
{
    public class SpecificationItemWithRequirementsQueryHandler : IQueryHandler<SpecificationItemWithRequirementsQuery, SpecificationItem>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemWithRequirementsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }    

        public SpecificationItem Handle(SpecificationItemWithRequirementsQuery query)
        {
            return _context.SpecificationItems
                           .Include(si => si.Requirements)
                           .FirstOrDefault(si => si.Id == query.Id);
        }
    }
}
