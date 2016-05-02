using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassDiagramContentViewModel
    {
        public string Locale { get; set; }
        public string Name { get; set; }

        public static ClassDiagramContentViewModel FromModel(ClassDiagramContent model)
        {
            return new ClassDiagramContentViewModel
            {
                Locale = model.Locale,
                Name = model.Name
            };
        }
    }
}
