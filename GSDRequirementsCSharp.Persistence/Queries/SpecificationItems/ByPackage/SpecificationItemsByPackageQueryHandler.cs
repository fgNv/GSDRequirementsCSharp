using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.ByPackage
{
    class SpecificationItemsByPackageQueryHandler : IQueryHandler<SpecificationItemsByPackageQuery, IEnumerable<SpecificationItem>>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemsByPackageQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<SpecificationItem> Handle(SpecificationItemsByPackageQuery query)
        {
            var specificationItems = _context.SpecificationItems
                                             .Where(s => s.PackageId == query.PackageId &&
                                                         s.Active)
                                             .ToList();

            return specificationItems;
        }
    }
}
