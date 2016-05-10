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

        public static UseCaseViewModel FromModel(UseCase model)
        {
            return new UseCaseViewModel
            {
                Id = model.Id,
                Contents = model.Contents.Select(UseCaseContentViewModel.FromModel)
            };
        }
    }
}
