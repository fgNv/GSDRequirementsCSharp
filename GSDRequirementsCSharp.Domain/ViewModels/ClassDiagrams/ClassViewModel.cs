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

        public IEnumerable<ClassPropertyViewModel> ClassProperties { get; set; }
        
        public IEnumerable<ClassMethodViewModel> ClassMethods { get; set; }

        public Guid Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public ClassType Type { get; set; }

        public static ClassViewModel FromModel(Class model)
        {
            return new ClassViewModel
            {
                Id = model.Id,
                Name = model.Name,
                ClassProperties = model.ClassProperties.Select(ClassPropertyViewModel.FromModel),
                ClassMethods = model.ClassMethods.Select(ClassMethodViewModel.FromModel),
                X = model.X,
                Y = model.Y,
                Type = model.Type
            };
        }
    }
}
