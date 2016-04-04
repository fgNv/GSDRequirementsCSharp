using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Requirements
{
    public class AddRequirementTranslationCommand : ICommand
    {
        public Guid Id { get; set; }

        [ValidateCollection]
        public IEnumerable<RequirementContentItem> Items { get; set; }
    }
}
