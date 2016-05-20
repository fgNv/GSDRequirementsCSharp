using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class ContactByUserIdQuery
    {
        public int UserId { get; set; }

        public static implicit operator ContactByUserIdQuery(int userId)
        {
            return new ContactByUserIdQuery { UserId = userId };
        }
    }
}
