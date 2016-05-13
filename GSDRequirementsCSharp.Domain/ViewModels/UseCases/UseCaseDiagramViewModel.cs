using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
{
    public class UseCaseDiagramViewModel : IIssueable
    {
        public Guid Id { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }

        public int? PackageIdentifier { get; set; }

        public int Identifier { get; set; }

        public int Version { get; set; }

        public IEnumerable<UseCaseDiagramContentViewModel> Contents { get; set; }

        public static UseCaseDiagramViewModel FromModel(UseCaseDiagram model)
        {
            return new UseCaseDiagramViewModel
            {
                Id = model.Id,
                Identifier = model.Identifier,
                PackageIdentifier = model.SpecificationItem?.Package?.Identifier,
                Contents = model.Contents.Select(UseCaseDiagramContentViewModel.FromModel),
                Version = model.Version
            };
        }
    }
}
