using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class SequenceDiagramNextIdQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator SequenceDiagramNextIdQuery(Guid projectId)
        {
            return new SequenceDiagramNextIdQuery { ProjectId = projectId };
        }
    }    
}
