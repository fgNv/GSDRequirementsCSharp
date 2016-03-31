using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.Paginated
{
    public class PackagesPaginatedQueryHandler : IQueryHandler<PackagesPaginatedQuery, PackagesPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly ICurrentLocaleName _currentLocaleName;

        public PackagesPaginatedQueryHandler(GSDRequirementsContext context,
                                             ICurrentProjectContextId currentProjectContextId,
                                             ICurrentLocaleName currentLocaleName)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
            _currentLocaleName = currentLocaleName;
        }

        private Package GetByIdAndLocale(IEnumerable<Package> packages, Guid id, string localeName)
        {
            return packages.FirstOrDefault(p => p.Id == id && p.Locale == localeName);
        }

        private Package DefinePackageForId(IEnumerable<Package> packages,
                                           string currentLocale,
                                           Guid id)
        {
            var currentLocalePackage =
                GetByIdAndLocale(packages, id, currentLocale);

            if (currentLocalePackage != null) return currentLocalePackage;

            var enUsLocalePackage = GetByIdAndLocale(packages,
                                                        id,
                                                        "en-US");
            if (enUsLocalePackage != null) return enUsLocalePackage;

            return packages.FirstOrDefault(p => p.Id == id);
        }

        public PackagesPaginatedQueryResult Handle(PackagesPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentProjectId = _currentProjectContextId.Get();

            var ids = _context.Packages
                              .Where(p => p.Project.Id == currentProjectId &&
                                          p.Active)
                              .OrderBy(p => p.Description)
                              .Select(p => p.Id)
                              .Distinct()
                              .ToList();

            var maxPages = (int)Math.Ceiling(ids.Count() / (double)query.PageSize);

            var paginatedIds = ids.Skip(skip).Take(query.PageSize).ToList();            

            var packages = _context.Packages
                                   .Where(p => paginatedIds.Contains(p.Id))
                                   .ToList();

            var currentLocale = _currentLocaleName.Get();

            Func<Guid,Package> definePackageForId = (id) => DefinePackageForId(packages, currentLocale, id);
            var packagesResult = ids.Select(definePackageForId);
            var result = new PackagesPaginatedQueryResult(packagesResult, maxPages);
            return result;
        }
    }
}
