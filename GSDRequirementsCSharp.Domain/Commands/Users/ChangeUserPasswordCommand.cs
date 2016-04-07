using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Users
{
    public class ChangeUserPasswordCommand : IUserCommand
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int UserId { get { return Id ?? 0; } }
    }
}
