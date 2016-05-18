using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System.Collections.Generic;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UseCaseItem
    {
        public Cell Cell { get; set; }
        [ValidateCollection]
        public IEnumerable<UseCaseContentItem> Contents { get; set; }
        [ValidateCollection]
        public IEnumerable<PostConditionData> PostConditions { get; set; }
        [ValidateCollection]
        public IEnumerable<PreConditionData> PreConditions { get; set; }
        public int? Identifier { get; set; }
    }
}
