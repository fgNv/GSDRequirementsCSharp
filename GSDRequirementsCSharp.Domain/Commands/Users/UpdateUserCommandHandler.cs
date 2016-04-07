using GSDRequirementsCSharp.Domain.Queries.Contacts;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Users
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IQueryHandler<ContactByUserIdQuery, Contact> _contactByUserIdQueryHandler;

        public UpdateUserCommandHandler(IQueryHandler<ContactByUserIdQuery, Contact> contactByUserIdQueryHandler)
        {
            _contactByUserIdQueryHandler = contactByUserIdQueryHandler;
        }

        public void Handle(UpdateUserCommand command)
        {
            var contact = _contactByUserIdQueryHandler.Handle(command.UserId);
            contact.Email = command.Email;
            contact.MobilePhone = command.MobilePhone;
            contact.Name = command.Name;
            contact.Phone = command.Phone;
        }
    }
}
