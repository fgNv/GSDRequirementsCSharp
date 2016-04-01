using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Domain.Queries.Packages
{
    public class PackageWithCurrentCultureQueryHandler : IQueryHandler<Guid, PackageWithCurrentCultureContentsQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentLocaleName _currentLocaleName;

        public PackageWithCurrentCultureQueryHandler(GSDRequirementsContext context,
                                                     ICurrentLocaleName currentLocaleName)
        {
            _context = context;
            _currentLocaleName = currentLocaleName;
        }

        public PackageWithCurrentCultureContentsQueryResult Handle(Guid id)
        {
            var package = _context.Packages
                                   .Include(p => p.Contents)
                                   .FirstOrDefault(p => p.Id == id);

            var result = new PackageWithCurrentCultureContentsQueryResult();

            var currentLocaleName = _currentLocaleName.Get();
            result.Package = package;
            result.PackageContent = package.Contents
                                           .FirstOrDefault(p => p.Locale == currentLocaleName);

            return result;
        }
    }
}
