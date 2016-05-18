using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class SpecificationItemWithRequirementsQueryResult
    {
        public SpecificationItem SpecificationItem { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
