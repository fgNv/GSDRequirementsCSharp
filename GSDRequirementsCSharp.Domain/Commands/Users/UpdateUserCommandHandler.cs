using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;

namespace GSDRequirementsCSharp.Domain.Commands
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
            contact.MobilePhone = command.MobilePhone.Replace(" ", "").Replace("+", "");
            contact.Name = command.Name;
            contact.Phone = command.Phone.Replace(" ", "").Replace("+", "");
        }
    }
}
