using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class PackageNextIdQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator PackageNextIdQuery(Guid projectId)
        {
            return new PackageNextIdQuery { ProjectId = projectId };
        }
    }
}
