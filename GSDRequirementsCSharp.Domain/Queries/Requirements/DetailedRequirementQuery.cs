using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class DetailedRequirementQuery
    {
        public Guid Id { get; set; }
        public int? Version { get; set; }

        public static implicit operator DetailedRequirementQuery(Guid id)
        {
            return new DetailedRequirementQuery { Id = id };
        }
    }
}
