using GSDRequirementsCSharp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Paginated
{
    public class ClassDiagramsPaginatedQueryResult
    {
        public IEnumerable<ClassDiagramViewModel> ClassDiagrams { get; set; }
        public int MaxPages { get; set; }

        public ClassDiagramsPaginatedQueryResult(IEnumerable<ClassDiagramViewModel> classDiagrams,
                                            int maxPages)
        {
            ClassDiagrams = classDiagrams;
            MaxPages = maxPages;
        }
    }
}
