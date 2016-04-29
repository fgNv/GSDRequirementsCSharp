using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassMethodViewModel
    {
        public string Name { get; set; }

        public string ReturnType { get; set; }

        public Visibility Visibility { get; set; }

        public IEnumerable<ParameterViewModel> ClassMethodParameters { get; set; }

        public static ClassMethodViewModel FromModel(ClassMethod model)
        {
            return new ClassMethodViewModel
            {
                Name = model.Name,
                Visibility = model.Visibility,
                ReturnType = model.ReturnType,
                ClassMethodParameters = model.ClassMethodParameters.Select(ParameterViewModel.FromModel)
            };
        }
    }
}
