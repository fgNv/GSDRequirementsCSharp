using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class SpecificationItemWithClassDiagramsQueryResult
    {
        public SpecificationItem SpecificationItem { get; set; }
        public IEnumerable<ClassDiagram> ClassDiagrams { get; set; }
    }
}
