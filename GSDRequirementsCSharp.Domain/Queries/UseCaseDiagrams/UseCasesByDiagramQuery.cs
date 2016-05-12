using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams
{
    public  class UseCasesByDiagramQuery
    {
        public Guid DiagramId { get; set; }

        public static implicit operator UseCasesByDiagramQuery (Guid id)
        {
            return new UseCasesByDiagramQuery { DiagramId = id };
        }
    }
}
