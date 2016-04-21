using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.WithContents
{
    internal class PackageWithContentsQueryHandler : IQueryHandler<Guid, Package>
    {
        private readonly GSDRequirementsContext _context;

        public PackageWithContentsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Package Handle(Guid id)
        {
            return _context.Packages.Include(p => p.Contents)
                           .FirstOrDefault(p => p.Id == id);
        }
    }
}
