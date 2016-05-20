using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class PreConditionData
    {
        [ValidateCollection]
        public IEnumerable<PreConditionContentItem> Contents { get; set; }
    }
}
