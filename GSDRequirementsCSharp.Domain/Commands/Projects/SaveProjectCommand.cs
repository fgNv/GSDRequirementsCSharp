using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Projects
{
    public class SaveProjectCommand : ICommand
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(65535)]
        [Required]
        public string Description { get; set; }
        
    }
}
