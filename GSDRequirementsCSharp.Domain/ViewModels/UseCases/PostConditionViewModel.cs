using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
{
    public class PostConditionViewModel
    {
        public IEnumerable<PostConditionContentViewModel> Contents { get; set; }

        public static PostConditionViewModel FromModel(UseCasePostCondition model)
        {
            return new PostConditionViewModel
            {
                Contents = model.Contents.Select(PostConditionContentViewModel.FromModel)
            };
        }
    }
}
