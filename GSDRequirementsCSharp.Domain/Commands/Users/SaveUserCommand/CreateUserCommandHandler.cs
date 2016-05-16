using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<Contact, Guid> _contactRepository;
        private readonly ICryptographer _cryptographer;

        public CreateUserCommandHandler(IRepository<User, int> userRepository,
                                        IRepository<Contact, Guid> contactRepository,
                                        ICryptographer cryptographer)
        {
            _userRepository = userRepository;
            _contactRepository = contactRepository;
            _cryptographer = cryptographer;
        }

        public void Handle(CreateUserCommand command)
        {
            var user = new User();
            user.Login = command.Login;
            user.Password = _cryptographer.Encrypt(command.Password);

            var contact = new Contact();
            contact.Email = command.Email;
            contact.Id = Guid.NewGuid();
            contact.MobilePhone = command.MobilePhone.Replace(" ", "").Replace("+", "");
            contact.Name = command.Name;
            contact.Phone = command.Phone.Replace(" ", "").Replace("+", "");

            user.Contact = contact;

            _contactRepository.Add(contact);
            _userRepository.Add(user);
        }
    }
}
