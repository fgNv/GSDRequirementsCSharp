using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand
{
    public class CreateUserCommand : ICommand
    {
        public CreateUserCommand()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        public Guid Id { get; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^([0-9]+)$")]
        public string MobilePhone { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^([0-9]+)$")]
        public string Phone { get; set; }
    }
}
