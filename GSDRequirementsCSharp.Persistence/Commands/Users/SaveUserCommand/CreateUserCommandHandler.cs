using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IRepository<Contact, Guid> _contactRepository;

        public CreateUserCommandHandler(IRepository<User, Guid> userRepository,
                                        IRepository<Contact, Guid> contactRepository)
        {
            _userRepository = userRepository;
            _userRepository = userRepository;
        }

        public void Handle(CreateUserCommand command)
        {
            var 
            _repository
        }
    }
}
