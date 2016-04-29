using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ClassPropertyViewModel> Properties { get; set; }
        
        public IEnumerable<ClassMethodViewModel> Methods { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public static ClassViewModel FromModel(Class model)
        {
            return new ClassViewModel
            {
                Name = model.Name,
                Properties = model.ClassProperties.Select(ClassPropertyViewModel.FromModel),
                Methods = model.ClassMethods.Select(ClassMethodViewModel.FromModel),
                X = model.X,
                Y = model.Y
            };
        }
    }
}
