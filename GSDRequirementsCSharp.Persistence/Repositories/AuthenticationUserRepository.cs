using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    internal class AuthenticationUserRepository : IUserRepository<User>
    {
        private readonly GSDRequirementsContext _context;

        public AuthenticationUserRepository(GSDRequirementsContext context)
        {
            _context = context;
        }

        public User Get(string login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login);
            return user;
        }
    }
}
