using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Projects
{
    public class CreateProjectCommand
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(65535)]
        public string Description { get; set; }
        
        [StringLength(10)]
        public string Locale { get; set; }

        public CreateProjectCommand()
        {
            Locale = Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}
