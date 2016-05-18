using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class ProjectNextIdQuery
    {
        public int UserId { get; set; }

        public static implicit operator ProjectNextIdQuery(int userId)
        {
            return new ProjectNextIdQuery { UserId = userId };
        }
    }
}
