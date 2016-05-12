using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
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
                SourceId = model.SourceId,
                TargetId = model.TargetId,
                Type = model.Type
            };
        }
    }
}
