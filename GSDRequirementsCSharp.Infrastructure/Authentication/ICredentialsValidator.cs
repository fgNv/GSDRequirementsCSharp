using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Authentication
{
    public interface ICredentialsValidator
    {
        void Validate(string username, string password);
    }
}
