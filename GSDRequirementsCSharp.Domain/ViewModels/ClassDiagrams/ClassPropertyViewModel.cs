using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassPropertyViewModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Visibility Visibility { get; set; }

        public static ClassPropertyViewModel FromModel(ClassProperty model)
        {
            return new ClassPropertyViewModel
            {
                Name = model.Name,
                Visibility = model.Visibility,
                Type = model.Type
            };
        }
    }
}
