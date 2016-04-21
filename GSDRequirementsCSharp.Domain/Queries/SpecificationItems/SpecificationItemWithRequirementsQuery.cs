using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.SpecificationItems
{
    public class SpecificationItemWithRequirementsQuery
    {
        public Guid Id { get; set; }
        public static implicit operator SpecificationItemWithRequirementsQuery(Guid id)
        {
            return new SpecificationItemWithRequirementsQuery { Id = id };
        }
    }
}
