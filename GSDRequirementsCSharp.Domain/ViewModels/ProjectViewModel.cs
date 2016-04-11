using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<ProjectContentViewModel> ProjectContents { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public bool IsUpdated { get; set; }

        public static ProjectViewModel FromModel(Project model)
        {
            return new ProjectViewModel
            {
                Id = model.Id,
                Name = model.GetName(),
                CreatedAt = model.CreatedAt,
                ProjectContents = model.ProjectContents.Select(ProjectContentViewModel.FromModel),
                Identifier = $"PJT{model.CreatorId}.{model.Identifier}"
            };
        }
    }
}
