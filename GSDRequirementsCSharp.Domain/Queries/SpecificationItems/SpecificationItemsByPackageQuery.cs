using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.SpecificationItems
{
    public class SpecificationItemsByPackageQuery
    {
        public Guid PackageId { get; set; }

        public static implicit operator SpecificationItemsByPackageQuery(Guid packageId)
        {
            return new SpecificationItemsByPackageQuery { PackageId = packageId };
        }
    }
}
