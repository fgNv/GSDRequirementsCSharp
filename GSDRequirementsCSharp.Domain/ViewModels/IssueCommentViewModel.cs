using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class IssueCommentViewModel
    {
        public Guid Id { get; set; }

        public DateTime LastModification { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }

        public string LastModificationLabel
        {
            get
            {
                var currentCultureFormat = Thread.CurrentThread
                                                 .CurrentUICulture
                                                 .DateTimeFormat;
                return LastModification.ToString(currentCultureFormat);
            }
        }

        public IEnumerable<IssueCommentContentViewModel> Contents { get; set; }

        public static IssueCommentViewModel FromModel(IssueComment model)
        {
            return new IssueCommentViewModel
            {
                Id = model.Id,
                LastModification = model.LastModification,
                CreatorId = model.CreatorId,
                CreatorName = model.Creator?.Contact?.Name,
                Contents = model.Contents?
                                .Select(IssueCommentContentViewModel.FromModel)
            };
        }
    }
}
