using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class UseCaseNextIdQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator UseCaseNextIdQuery(Guid id)
        {
            return new UseCaseNextIdQuery { ProjectId = id };
        }
    }
}
