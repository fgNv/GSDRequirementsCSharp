using GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams.DTO;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class ActorItem
    {
        public Cell Cell { get; set; }
        [ValidateCollection]
        public IEnumerable<ActorContentItem> Contents { get; set; }
    }
}
