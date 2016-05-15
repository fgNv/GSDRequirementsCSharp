using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Globals;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System.Resources;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class AuditingViewModel
    {
        public string RelativeTime { get; set; }
        public string UserName { get; set; }
        public string DateExecuted { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public Guid ProjectId { get; set; }

        public static AuditingViewModel FromModel(Auditing model)
        {
            return new AuditingViewModel
            {
                RelativeTime = model.ExecutedAt.RelativeTime(),
                DateExecuted = $"{model.ExecutedAt:dd/MM/yyyy hh:mm}",
                Description = Sentences.ResourceManager.GetString(model.ActivityDescription),
                ProjectId = model.ProjectId,
                ProjectName = model.Project?.GetName(),
                UserName = model.User?.Contact.Name
            };
        }
    }
}
