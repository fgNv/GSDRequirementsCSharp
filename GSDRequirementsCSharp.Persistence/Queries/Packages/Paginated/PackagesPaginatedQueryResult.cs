using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.Paginated
{
    public class PackagesPaginatedQueryResult
    {
        public IEnumerable<PackageViewModel> Packages { get; set; }
        public int MaxPages { get; set; }

        public PackagesPaginatedQueryResult(IEnumerable<PackageViewModel> packages,
                                            int maxPages)
        {
            Packages = packages;
            MaxPages = maxPages;
        }
    }
}
