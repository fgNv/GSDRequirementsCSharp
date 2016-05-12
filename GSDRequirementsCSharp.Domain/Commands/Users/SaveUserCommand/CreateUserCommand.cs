using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GSDRequirementsCSharp.Infrastructure.Internationalization;

namespace GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand
{
    public class CreateUserCommand : ICommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.loginIsARequiredField))]
        [StringLength(50,
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.maxLoginLengthIs50))]
        public string Login { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.passwordIsARequiredField))]
        [StringLength(50,
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.maxPasswordLengthIs50))]
        [MinLength(4,
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.minPasswordLengthIs4))]
        public string Password { get; set; }

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
