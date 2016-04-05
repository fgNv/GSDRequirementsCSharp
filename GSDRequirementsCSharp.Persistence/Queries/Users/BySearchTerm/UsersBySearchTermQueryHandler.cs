using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Users.BySearchTerm
{
    internal class UsersBySearchTermQueryHandler : IQueryHandler<UsersBySearchTermQuery, IEnumerable<UserViewModel>>
    {
        private GSDRequirementsContext _context;

        public UsersBySearchTermQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<UserViewModel> Handle(UsersBySearchTermQuery query)
        {
            var dbQuery = _context.Users
                                .Include(u => u.Contact)
                                .Where(u => u.Contact.Email.Contains(query.SearchTerm) ||
                                            u.Contact.Name.Contains(query.SearchTerm))
                                .Select(u => new UserViewModel
                                {
                                    Id = u.Id,
                                    Name = u.Contact.Name,
                                    Email = u.Contact.Email
                                });

            return dbQuery.ToList();
        }
    }
}
