using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassDiagramDetailedViewModel
    {
        public Guid Id { get; set; }

        public IEnumerable<ClassDiagramContentViewModel> Contents { get; set; }

        public int Identifier { get; set; }

        public int Version { get; set; }

        public Guid PackageId { get; set; }

        public IEnumerable<ClassViewModel> Classes { get; set; }

        public IEnumerable<ClassRelationshipViewModel> Relations { get; set; }

        public static ClassDiagramDetailedViewModel FromModel(ClassDiagram model)
        {
            return new ClassDiagramDetailedViewModel
            {
                Id = model.Id,
                Identifier = model.Identifier,
                Version = model.Version,
                PackageId = model.SpecificationItem.PackageId,
                Contents = model.Contents.Select(ClassDiagramContentViewModel.FromModel),
                Classes = model.Classes.Select(ClassViewModel.FromModel),
                Relations = model.Relationships.Select(ClassRelationshipViewModel.FromModel)
            };
        }
    }
}
