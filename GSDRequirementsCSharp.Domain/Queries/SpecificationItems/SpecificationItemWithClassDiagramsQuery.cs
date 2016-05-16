using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.SpecificationItems
{
    public class SpecificationItemWithClassDiagramsQuery
    {
        public Guid Id { get; set; }
        public static implicit operator SpecificationItemWithClassDiagramsQuery(Guid id)
        {
            return new SpecificationItemWithClassDiagramsQuery { Id = id };
        }
    }
}
