using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GSDRequirementsCSharp.Persistence.Authentication
{
    public class CurrentUserRetriever : ICurrentUserRetriever<User>
    {
        private readonly GSDRequirementsContext _context;

        public CurrentUserRetriever(GSDRequirementsContext context)
        {
            _context = context;
        }

        public User Get()
        {
            var username = HttpContext.Current.User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Login == username);
            return user;
        }
    }
}
