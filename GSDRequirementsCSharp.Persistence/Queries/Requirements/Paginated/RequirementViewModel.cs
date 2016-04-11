using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class RequirementViewModel
    {
        public int Identifier { get; set; }
        public RequirementType Type { get; set; }
        public RequirementType RequirementType { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Prefix { get; set; }
        public Guid PackageId { get; set; }
        public Package Package { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<RequirementContent> RequirementContents { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
    }
}
