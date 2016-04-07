using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Users
{
    public class UpdateUserCommand : IUserCommand
    {
        [Required]
        public int? Id { get; }

        public int UserId { get { return Id ?? 0; } }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        
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
