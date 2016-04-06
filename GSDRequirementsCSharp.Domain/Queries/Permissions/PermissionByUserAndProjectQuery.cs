using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Permissions
{
    public class PermissionByUserAndProjectQuery
    {
        public Guid ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
