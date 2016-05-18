using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System.Collections.Generic;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class ActorItem
    {
        public Cell Cell { get; set; }
        [ValidateCollection]
        public IEnumerable<ActorContentItem> Contents { get; set; }
    }
}
