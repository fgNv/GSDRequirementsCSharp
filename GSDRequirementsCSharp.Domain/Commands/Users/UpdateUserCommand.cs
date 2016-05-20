using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System.ComponentModel.DataAnnotations;
using static GSDRequirementsCSharp.Persistence.Commands.CreateUserCommand;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UpdateUserCommand : ICommand
    {
        [Required(
          ErrorMessageResourceType = typeof(ValidationMessages),
          ErrorMessageResourceName = nameof(ValidationMessages.emailIsARequiredField))]
        [EmailAddress(
          ErrorMessageResourceType = typeof(ValidationMessages),
          ErrorMessageResourceName = nameof(ValidationMessages.emailFieldMustHaveAValidEmailAddress))]
        [StringLength(100,
          ErrorMessageResourceType = typeof(ValidationMessages),
          ErrorMessageResourceName = nameof(ValidationMessages.maxEmailLengthIs50))]
        public string Email { get; set; }

        [StringLength(23,
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = nameof(ValidationMessages.maxMobilePhoneLengthIs20))]
        [RegularExpression(PHONE_REGULAR_EXPRESSION,
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = nameof(ValidationMessages.mobilePhoneMustContainOnlyNumbers))]
        public string MobilePhone { get; set; }

        [Required(
      ErrorMessageResourceType = typeof(ValidationMessages),
      ErrorMessageResourceName = nameof(ValidationMessages.nameIsARequiredField))]
        [StringLength(100,
      ErrorMessageResourceType = typeof(ValidationMessages),
      ErrorMessageResourceName = nameof(ValidationMessages.maxNameLengthIs100))]
        public string Name { get; set; }

        [StringLength(23,
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.maxPhoneLengthIs20))]
        [RegularExpression(PHONE_REGULAR_EXPRESSION,
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.phoneMustContainOnlyNumbers))]
        public string Phone { get; set; }
    }
}
