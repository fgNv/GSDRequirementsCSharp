using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class UseCaseArtifactViewModel
    {
        public UseCaseViewModel UseCase { get; set; }
        public SpecificationItemViewModel SpecificationItem { get; set; }

        public static UseCaseArtifactViewModel FromModel(UseCase useCase, SpecificationItem specificationItem)
        {
            return new UseCaseArtifactViewModel
            {
                UseCase = UseCaseViewModel.FromModel(useCase),
                SpecificationItem = SpecificationItemViewModel.FromModel(specificationItem)
            };
        }
    }
}
