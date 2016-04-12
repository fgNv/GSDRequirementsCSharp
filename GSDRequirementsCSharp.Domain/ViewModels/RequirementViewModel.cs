using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class RequirementViewModel
    {
        public int Identifier { get; set; }
        public RequirementType Type { get; set; }
        public RequirementType RequirementType { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Prefix { get; set; }
        public Guid PackageId { get; set; }
        public PackageViewModel Package { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public int Rank { get; set; }
        public IEnumerable<RequirementContentViewModel> RequirementContents { get; set; }
        public IEnumerable<IssueViewModel> Issues { get; set; }

        public static RequirementViewModel FromModel(Requirement model)
        {
            return new RequirementViewModel
            {
                RequirementContents = model.RequirementContents?
                                           .Select(RequirementContentViewModel.FromModel),
                Difficulty = model.Difficulty,
                Id = model.Id,
                Package = PackageViewModel.FromModel(model.SpecificationItem.Package),
                Identifier = model.Identifier,
                RequirementType = model.Type,
                Type = model.Type,
                Rank = model.Rank
            };
        }
    }
}
