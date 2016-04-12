using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class IssueCommentViewModel
    {
        public Guid Id { get; set; }

        public DateTime LastModification { get; set; }

        public IEnumerable<IssueCommentContentViewModel> Contents { get; set; }

        public static IssueCommentViewModel FromModel(IssueComment model)
        {
            return new IssueCommentViewModel
            {
                Id = model.Id,
                LastModification = model.LastModification/*,
                Contents = model.Contents?
                                .Select(IssueCommentContentViewModel.FromModel)*/
            };
        }
    }
}
