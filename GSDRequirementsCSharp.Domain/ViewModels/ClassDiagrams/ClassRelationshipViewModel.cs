using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassRelationshipViewModel
    {
        public Guid SourceId { get; set; }
        public string SourceMultiplicity { get; set; }
        public RelationType Type { get; set; }
        public Guid TargetId { get; set; }
        public string TargetMultiplicity { get; set; }

        public static ClassRelationshipViewModel FromModel(ClassRelationship model)
        {
            return new ClassRelationshipViewModel
            {
                SourceId = model.SourceId,
                TargetId = model.TargetId,
                TargetMultiplicity = model.TargetMultiplicity,
                SourceMultiplicity = model.SourceMultiplicity,
                Type = model.Type
            };
        }
    }
}
