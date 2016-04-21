using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class SpecificationItemIssuesQuery
    {
        public Guid SpecificationItemId { get; set; }

        public static implicit operator SpecificationItemIssuesQuery(Guid id)
        {
            return new SpecificationItemIssuesQuery { SpecificationItemId = id };
        }
    }
}
