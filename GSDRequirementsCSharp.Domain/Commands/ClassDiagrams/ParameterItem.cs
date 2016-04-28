﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class ParameterItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
