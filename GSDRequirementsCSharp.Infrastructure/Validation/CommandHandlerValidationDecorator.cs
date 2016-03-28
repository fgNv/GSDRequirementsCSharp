using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation
{
    public class CommandHandlerValidationDecorator<T> : ICommandHandler<T>
        where T : class, ICommand
    {
        private IValidator _validator;
        private ICommandHandler<T> _decorated;

        public CommandHandlerValidationDecorator(IValidator validator,
                                                 ICommandHandler<T> decorated)
        {
            _validator = validator;
            _decorated = decorated;
        }

        public void Handle(T command)
        {
            var errors = _validator.Validate(command);
            if (errors.Any())
                throw new CommandValidationException(errors);

            _decorated.Handle(command);
        }
    }
}
