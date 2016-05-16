using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ParameterViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public static ParameterViewModel FromModel(ClassMethodParameter model)
        {
            return new ParameterViewModel
            {
                Name = model.Name,
                Type = model.Type
            };
        }
    }
}
