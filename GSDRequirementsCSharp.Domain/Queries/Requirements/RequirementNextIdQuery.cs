using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Requirements
{
    public class RequirementNextIdQuery
    {
        [Required]
        public Guid? ProjectId { get; set; }

        [Required]
        public RequirementType? RequirementType { get; set; }        
    }
}
