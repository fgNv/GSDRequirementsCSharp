using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class IssueCommentContentViewModel
    {
        public Guid Id { get; set; }
        
        public string Locale { get; set; }
        
        public string Description { get; set; }

        public bool IsUpdated { get; set; }

        public static IssueCommentContentViewModel FromModel(IssueCommentContent model)
        {
            return new IssueCommentContentViewModel
            {
                Id = model.Id,
                Locale = model.Locale,
                Description = model.Description,
                IsUpdated = model.IsUpdated
            };
        }
    }
}
