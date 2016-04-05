using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Users
{
    internal class UserByLoginQueryHandler : IQueryHandler<string, User>
    {
        private readonly GSDRequirementsContext _context;

        public UserByLoginQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public User Handle(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == username);
            return user;
        }
    }
}
