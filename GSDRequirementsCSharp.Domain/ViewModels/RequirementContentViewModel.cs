using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class RequirementContentViewModel
    {
        public Guid Id { get; set; }
                
        public string Action { get; set; }
        
        public string Condition { get; set; }
        
        public string Subject { get; set; }

        public string Locale { get; set; }

        public int Version { get; set; }

        public static RequirementContentViewModel FromModel(RequirementContent model)
        {
            if (model == null) return null;

            return new RequirementContentViewModel
            {
                Id = model.Id,
                Locale = model.Locale,
                Subject = model.Subject,
                Action = model.Action,
                Condition = model.Condition,
                Version = model.Version
            };
        }
    }
}
