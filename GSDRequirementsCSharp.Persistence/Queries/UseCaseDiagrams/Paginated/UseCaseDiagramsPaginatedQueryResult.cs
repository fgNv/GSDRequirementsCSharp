using GSDRequirementsCSharp.Domain.ViewModels;
using System.Collections.Generic;

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
