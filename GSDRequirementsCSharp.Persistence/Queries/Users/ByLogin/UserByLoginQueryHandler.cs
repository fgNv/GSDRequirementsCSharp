using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.Users.ByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Users
{
    internal class UserByLoginQueryHandler : IQueryHandler<UserByLoginQuery, User>
    {
        private readonly GSDRequirementsContext _context;

        public UserByLoginQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public User Handle(UserByLoginQuery query)
        {
            var user = _context.Users
                               .FirstOrDefault(u => u.Login == query.Login);
            return user;
        }
    }
}
