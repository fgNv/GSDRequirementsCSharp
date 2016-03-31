using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Packages.ByGuid
{
    public class PackagesByGuidQueryHandler : IQueryHandler<Guid, IEnumerable<Package>>
    {
        private readonly GSDRequirementsContext _context;

        public PackagesByGuidQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<Package> Handle(Guid id)
        {
            var packages = _context.Packages
                                   .Where(p => p.Id == id)
                                   .ToList();
            return packages;
        }
    }
}
