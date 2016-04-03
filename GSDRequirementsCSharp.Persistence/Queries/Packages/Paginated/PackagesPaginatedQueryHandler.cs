using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.Paginated
{
    public class PackagesPaginatedQueryHandler : IQueryHandler<PackagesPaginatedQuery, PackagesPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public PackagesPaginatedQueryHandler(GSDRequirementsContext context,
                                             ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public PackagesPaginatedQueryResult Handle(PackagesPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentProjectId = _currentProjectContextId.Get();

            var packagesQuery = _context.Packages
                                 .Where(p => p.Project.Id == currentProjectId && p.Active);

            var maxPages = (int)Math.Ceiling(packagesQuery.Count() / (double)query.PageSize);

            var packages = packagesQuery.OrderBy(p => p.Identifier)
                                        .Include(p => p.Contents)
                                        .Skip(skip)
                                        .Take(query.PageSize)
                                        .ToList();
                        
            var result = new PackagesPaginatedQueryResult(packages, maxPages);
            return result;
        }
    }
}
