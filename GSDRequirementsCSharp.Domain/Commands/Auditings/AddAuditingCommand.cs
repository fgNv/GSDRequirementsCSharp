using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class AddAuditingCommand : ICommand
    {
        [Required]
        public int? UserId { get; set; }

        [Required]
        public Guid? ProjectId { get; set; }
        public string Description { get; set; }
    }
}
