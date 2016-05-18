using GSDRequirementsCSharp.Domain.Commands;
using System.ComponentModel.DataAnnotations;

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
