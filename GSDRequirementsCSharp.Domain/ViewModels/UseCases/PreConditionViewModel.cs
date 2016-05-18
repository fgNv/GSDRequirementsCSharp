using GSDRequirementsCSharp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class PreConditionViewModel
    {
        public IEnumerable<PreConditionContentViewModel> Contents { get; set; }

        public static PreConditionViewModel FromModel(UseCasePreCondition model)
        {
            return new PreConditionViewModel
            {
                Contents = model.Contents.Select(PreConditionContentViewModel.FromModel)
            };
        }
    }
}
