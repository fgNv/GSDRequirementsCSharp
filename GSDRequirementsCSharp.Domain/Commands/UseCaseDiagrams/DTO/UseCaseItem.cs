using GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams.DTO;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
