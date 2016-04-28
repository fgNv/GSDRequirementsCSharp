using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class MethodItem
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ReturnType { get; set; }

        public IEnumerable<ParameterItem> Parameters { get; set; }
    }
}
