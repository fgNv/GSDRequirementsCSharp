using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams
{
    public class UseCaseDiagramsPaginatedQueryResult
    {
        public IEnumerable<UseCaseDiagramViewModel> UseCaseDiagrams { get; set; }
        public int MaxPages { get; set; }

        public UseCaseDiagramsPaginatedQueryResult(IEnumerable<UseCaseDiagramViewModel> useCaseDiagrams,
                                            int maxPages)
        {
            UseCaseDiagrams = useCaseDiagrams;
            MaxPages = maxPages;
        }
    }
}
