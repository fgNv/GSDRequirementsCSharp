using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class SpecificationItemViewModel
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public PackageViewModel Package { get; set; }

        public string TypeLabel { get; set; }

        public SpecificationItemType Type { get; set; }

        public static string GetTypeLabel(SpecificationItemType type)
        {
            switch (type)
            {
                case SpecificationItemType.ClassDiagram:
                    return Sentences.classDiagram;
                case SpecificationItemType.Requirement:
                    return Sentences.requirement;
                case SpecificationItemType.UseCaseDiagram:
                    return Sentences.useCaseDiagram;
                case SpecificationItemType.UseCase:
                    return Sentences.useCase;
                case SpecificationItemType.SequenceDiagram:
                    return Sentences.sequenceDiagram;
            }
            return "";
        }

        public static SpecificationItemViewModel FromModel(SpecificationItem item)
        {
            return new SpecificationItemViewModel
            {
                Id = item.Id,
                Package = PackageViewModel.FromModel(item.Package),
                Label = item.Label,
                Type = item.Type,
                TypeLabel = GetTypeLabel(item.Type),
            };
        }
    }
}
