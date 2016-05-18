using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.Queries;

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
