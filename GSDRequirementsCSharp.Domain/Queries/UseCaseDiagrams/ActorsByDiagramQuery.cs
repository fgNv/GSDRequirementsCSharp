using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class ActorsByDiagramQuery
    {
        public Guid DiagramId { get; set; }
        public int? Version { get; set; }
    }
}
