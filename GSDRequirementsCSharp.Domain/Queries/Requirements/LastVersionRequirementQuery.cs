using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Requirements
{
    public class LastVersionRequirementQuery
    {
        public Guid Id { get; set; }

        public static implicit operator LastVersionRequirementQuery(Guid id)
        {
            return new LastVersionRequirementQuery { Id = id };
        }
    }
}
