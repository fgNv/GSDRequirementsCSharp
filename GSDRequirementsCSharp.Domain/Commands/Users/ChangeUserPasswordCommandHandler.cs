using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Users
{
    public class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand>
    {
        private readonly ICryptographer _cryptographer;
        private readonly IRepository<User, int> _userRepository;

        public ChangeUserPasswordCommandHandler(ICryptographer cryptographer,
                                                IRepository<User, int> userRepository)
        {
            _cryptographer = cryptographer;
            _userRepository = userRepository;
        }

        public void Handle(ChangeUserPasswordCommand command)
        {
            var user = _userRepository.Get(command.UserId);
            user.Password = _cryptographer.Encrypt(command.Password);
        }
    }
}
