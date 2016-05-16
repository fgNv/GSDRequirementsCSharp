using GSDRequirementsCSharp.Domain.Commands.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Validation
{
    public class RequirementDescriptionRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var command = value as RequirementContentItem;
            if (command == null)
                return true;

            return !string.IsNullOrWhiteSpace(command.Action) ||
                !string.IsNullOrWhiteSpace(command.Condition) ||
                !string.IsNullOrWhiteSpace(command.Subject);
        }
    }
}
