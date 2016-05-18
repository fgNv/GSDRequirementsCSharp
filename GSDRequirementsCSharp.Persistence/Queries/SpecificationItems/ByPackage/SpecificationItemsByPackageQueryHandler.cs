using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Collections.Generic;
using System.Linq;

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
