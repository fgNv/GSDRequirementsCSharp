using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.SpecificationItems
{
    public class SpecificationItemWithUseCaseDiagramsQueryResult
    {
        public SpecificationItem SpecificationItem { get; set; }
        public IEnumerable<UseCaseDiagram> UseCaseDiagrams { get; set; }
    }
}
