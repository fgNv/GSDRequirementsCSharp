using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Authentication
{
    public interface IUserRepository<TUser> where TUser : class
    {
        TUser Get(string login);
    }
}
