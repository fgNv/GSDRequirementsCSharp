using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
{
    public class UseCaseViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<UseCaseContentViewModel> Contents { get; set; }
        public IEnumerable<PostConditionViewModel> PostConditions { get; set; }
        public IEnumerable<PreConditionViewModel> PreConditions { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public static UseCaseViewModel FromModel(UseCase model)
        {
            return new UseCaseViewModel
            {
                Id = model.Id,
                Contents = model.Contents.Select(UseCaseContentViewModel.FromModel),
                PreConditions = model.PreConditions.Select(PreConditionViewModel.FromModel),
                PostConditions = model.PostConditions.Select(PostConditionViewModel.FromModel),
                X = model.X,
                Y = model.Y
            };
        }
    }
}
