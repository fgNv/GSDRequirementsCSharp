using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams.DTO
{
    public class PostConditionData
    {
        [ValidateCollection]
        public IEnumerable<PostConditionContentItem> Contents { get; set; }
    }
}
