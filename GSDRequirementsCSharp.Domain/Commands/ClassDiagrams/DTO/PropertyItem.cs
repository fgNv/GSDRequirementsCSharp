using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class PropertyItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Visibility? Visibility { get; set; }
    }
}
