using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation
{
    public class CommandValidationException : Exception
    {
        private readonly IEnumerable<string> _messages;

        public CommandValidationException(IEnumerable<string> messages)
        {
            _messages = messages;
        }

        public IEnumerable<string> Messages { get { return _messages; } }
    }
}
