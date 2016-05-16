using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
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
