using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class RequirementsPaginatedQueryResult
    {
        public IEnumerable<Requirement> Requirements { get; set; }
        public int MaxPages { get; set; }

        public RequirementsPaginatedQueryResult(IEnumerable<Requirement> requirements,
                                            int maxPages)
        {
            Requirements = requirements;
            MaxPages = maxPages;
        }
    }
}
