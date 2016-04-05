using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
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
        private readonly IQueryHandler<string, User> _userByLoginQueryHandler;

        public CurrentUserRetriever(IQueryHandler<string, User> userByLoginQueryHandler)
        {
            _userByLoginQueryHandler = userByLoginQueryHandler;
        }

        public User Get()
        {
            var username = HttpContext.Current.User.Identity.Name;
            var user = _userByLoginQueryHandler.Handle(username);
            return user;
        }
    }
}
