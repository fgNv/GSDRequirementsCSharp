using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class UseCaseEntityRelationViewModel
    {
        public Guid SourceId { get; set; }
        public Guid TargetId { get; set; }
        public IEnumerable<UseCaseEntityRelationContentViewModel> Contents { get; set; }

        public static UseCaseEntityRelationViewModel FromModel(UseCaseEntityRelation model)
        {
            return new UseCaseEntityRelationViewModel
            {
                SourceId = model.Source.Id,
                TargetId = model.Target.Id,
                Contents = model.Contents.Select(UseCaseEntityRelationContentViewModel.FromModel)
            };
        }
    }
}
