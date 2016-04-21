using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Users.ByLogin
{
    public class UserByLoginQuery
    {
        public string Login { get; set; }
        
        public static implicit operator UserByLoginQuery(string login)
        {
            return new UserByLoginQuery { Login = login };
        }
    }
}
