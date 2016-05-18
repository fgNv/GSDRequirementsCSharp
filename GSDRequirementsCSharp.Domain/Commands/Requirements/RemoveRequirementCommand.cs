using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    [CommandDescription(nameof(Sentences.requirementRemoved))]
    public class RemoveRequirementCommand : IProjectCommand
    {
        public Guid Id { get; set; }
    }
}
