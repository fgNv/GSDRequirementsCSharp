using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Requirements
{
    public class RequirementNextIdQuery
    {
        public Guid ProjectId { get; set; }
        public RequirementType RequirementType { get; set; }
        
    }
}
