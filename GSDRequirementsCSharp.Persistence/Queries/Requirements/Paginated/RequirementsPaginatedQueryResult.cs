using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class RequirementsPaginatedQueryResult
    {
        public IEnumerable<RequirementViewModel> Requirements { get; set; }
        public int MaxPages { get; set; }

        public RequirementsPaginatedQueryResult(IEnumerable<RequirementViewModel> requirements,
                                            int maxPages)
        {
            Requirements = requirements;
            MaxPages = maxPages;
        }
    }
}
