using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassDiagramViewModel
    {
        public Guid Id { get; set; }

        public static ClassDiagramViewModel FromModel(ClassDiagram model)
        {
            return new ClassDiagramViewModel
            {
                Id = model.Id
            };
        }
    }
}
