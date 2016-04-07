using GSDRequirementsCSharp.Domain.Queries.Contacts;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
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
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public UpdateUserCommandHandler(IQueryHandler<ContactByUserIdQuery, Contact> contactByUserIdQueryHandler,
                                        ICurrentUserRetriever<User> currentUserRetriever)
        {
            _contactByUserIdQueryHandler = contactByUserIdQueryHandler;
            _currentUserRetriever = currentUserRetriever;
        }

        public void Handle(UpdateUserCommand command)
        {
            var user = _currentUserRetriever.Get();
            var contact = _contactByUserIdQueryHandler.Handle(user.Id);
            contact.Email = command.Email;
            contact.MobilePhone = command.MobilePhone;
            contact.Name = command.Name;
            contact.Phone = command.Phone;
        }
    }
}
