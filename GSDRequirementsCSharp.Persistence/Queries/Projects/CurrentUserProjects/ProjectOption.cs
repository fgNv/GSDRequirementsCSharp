using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class ProjectOption
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static ProjectOption FromModel(Project model)
        {
            return new ProjectOption
            {
                Id = model.Id,
                Name = model.GetName()
            };
        }
    }
}
