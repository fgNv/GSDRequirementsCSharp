using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.ByProject
{
    public class ItemsByProjectQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator ItemsByProjectQuery(Guid projectId)
        {
            return new ItemsByProjectQuery { ProjectId = projectId };
        }
    }
}
