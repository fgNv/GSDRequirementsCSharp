using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ProjectContentViewModel
    {
        public Guid Id { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsUpdated { get; set; }

        public static ProjectContentViewModel FromModel(ProjectContent model)
        {
            return new ProjectContentViewModel
            {
                Id = model.Id,
                Locale = model.Locale,
                IsUpdated = model.IsUpdated,
                Name = model.Name,
                Description = model.Description
            };
        }
    }
}
