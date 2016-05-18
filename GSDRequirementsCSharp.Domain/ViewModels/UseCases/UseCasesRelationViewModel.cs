using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class UseCasesRelationViewModel
    {
        public Guid SourceId { get; set; }
        public Guid TargetId { get; set; }
        public UseCasesRelationType Type { get; set; }

        public static UseCasesRelationViewModel FromModel(UseCasesRelation model)
        {
            return new UseCasesRelationViewModel
            {
                SourceId = model.Source.Id,
                TargetId = model.Target.Id,
                Type = model.Type
            };
        }
    }
}
