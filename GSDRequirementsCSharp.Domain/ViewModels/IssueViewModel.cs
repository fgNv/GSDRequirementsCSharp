using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class IssueViewModel
    {
        public Guid Id { get; set; }

        public Guid SpecificationItemId { get; set; }

        public IEnumerable<IssueContentViewModel> Contents { get; set; }

        public IEnumerable<IssueCommentViewModel> Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModification { get; set; }

        public static IssueViewModel FromModel(Issue model)
        {
            var vm = new IssueViewModel
            {
                CreatedAt = model.CreatedAt,
                Id = model.Id,
                LastModification = model.LastModification,
                SpecificationItemId = model.SpecificationItemId,
                Contents = model.Contents?
                                .Select(IssueContentViewModel.FromModel),
                Comments = model.IssueComments?
                               .Select(IssueCommentViewModel.FromModel)
        };
            return vm;
        }
    }
}
