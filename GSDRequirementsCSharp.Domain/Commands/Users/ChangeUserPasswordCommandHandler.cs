using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
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
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public ChangeUserPasswordCommandHandler(ICryptographer cryptographer,
                                                ICurrentUserRetriever<User> currentUserRetriever)
        {
            _cryptographer = cryptographer;
            _currentUserRetriever = currentUserRetriever;
        }

        public void Handle(ChangeUserPasswordCommand command)
        {
            var user = _currentUserRetriever.Get();
            user.Password = _cryptographer.Encrypt(command.Password);
        }
    }
}
