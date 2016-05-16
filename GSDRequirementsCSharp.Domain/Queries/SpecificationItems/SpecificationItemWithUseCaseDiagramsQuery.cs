using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams
{
    public class SpecificationItemWithUseCaseDiagramsQuery
    {
        public Guid Id { get; set; }

        public static implicit operator SpecificationItemWithUseCaseDiagramsQuery(Guid id)
        {
            return new SpecificationItemWithUseCaseDiagramsQuery { Id = id };
        }
    }
}
