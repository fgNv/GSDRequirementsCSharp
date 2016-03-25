using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Cryptography;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Authentication
{
    public class LocalCredentialsValidator : ICredentialsValidator
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly ICryptographer _cryptographer;

        public LocalCredentialsValidator(IUserRepository<User> userRepository, ICryptographer cryptographer)
        {
            _userRepository = userRepository;
            _cryptographer = cryptographer;
        }

        public void Validate(string username, string password)
        {
            var user = _userRepository.Get(username);
            if (user == null)
                throw new NotificationException(Sentences.authenticationFailed,
                                                Sentences.userNotFound,
                                                NoteType.Warning);

            if (user.password != _cryptographer.Encrypt(password))
                throw new NotificationException(Sentences.authenticationFailed,
                                                Sentences.invalidPassword,
                                                NoteType.Warning);
        }
    }
}
