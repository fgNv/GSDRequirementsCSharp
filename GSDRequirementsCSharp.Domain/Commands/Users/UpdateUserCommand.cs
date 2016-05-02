using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Users
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

        [StringLength(20,
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = nameof(ValidationMessages.maxMobilePhoneLengthIs50))]
        [RegularExpression(@"^([0-9]+)$",
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

        [StringLength(20,
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.maxPhoneLengthIs20))]
        [RegularExpression(@"^([0-9]+)$",
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.phoneMustContainOnlyNumbers))]
        public string Phone { get; set; }
    }
}
