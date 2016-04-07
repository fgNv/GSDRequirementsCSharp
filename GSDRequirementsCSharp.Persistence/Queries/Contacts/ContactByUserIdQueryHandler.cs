using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.Contacts;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Contacts
{
    class ContactByUserIdQueryHandler : IQueryHandler<ContactByUserIdQuery, Contact>
    {
        private readonly GSDRequirementsContext _context;

        public ContactByUserIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Contact Handle(ContactByUserIdQuery query)
        {
            var user = _context.Users
                           .Include(u => u.Contact)
                           .FirstOrDefault(u => u.Id == query.UserId);

            if (user == null)
                return null;

            return user.Contact;
        }
    }
}
