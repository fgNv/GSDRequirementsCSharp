using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.Paginated
{
    public class PackagesPaginatedQueryResult
    {
        public IEnumerable<Package> Packages { get; set; }
        public int MaxPages { get; set; }

        public PackagesPaginatedQueryResult(IEnumerable<Package> packages,
                                            int maxPages)
        {
            Packages = packages;
            MaxPages = maxPages;
        }
    }
}
