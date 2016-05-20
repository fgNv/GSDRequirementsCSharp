using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class CreateProjectCommand : ICommand
    {
        [ValidateCollection]
        public IEnumerable<ProjectContentItem> Items { get; set; }
    }
}
