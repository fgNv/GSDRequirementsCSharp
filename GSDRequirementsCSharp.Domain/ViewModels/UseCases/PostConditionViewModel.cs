using GSDRequirementsCSharp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.ViewModels
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
