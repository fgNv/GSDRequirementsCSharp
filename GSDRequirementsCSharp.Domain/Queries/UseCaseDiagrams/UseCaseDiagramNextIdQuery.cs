using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class UseCaseDiagramNextIdQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator UseCaseDiagramNextIdQuery(Guid id)
        {
            return new UseCaseDiagramNextIdQuery { ProjectId = id };
        }
    }
}
