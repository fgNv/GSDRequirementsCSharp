using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class IssueContentViewModel
    {
        public Guid Id { get; set; }
        public string Locale { get; set; }
        public string Description { get; set; }

        public static IssueContentViewModel FromModel(IssueContent model)
        {
            return new IssueContentViewModel
            {
                Id = model.Id,
                Description = model.Description,
                Locale = model.Locale
            };
        }
    }
}
