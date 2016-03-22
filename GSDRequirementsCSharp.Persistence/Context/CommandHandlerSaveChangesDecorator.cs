using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Context
{
    public class CommandHandlerSaveChangesDecorator<T> :
        ICommandHandler<T>
        where T : ICommand
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICommandHandler<T> _decorated;

        public CommandHandlerSaveChangesDecorator(GSDRequirementsContext context,
                                                  ICommandHandler<T> decorated)
        {
            _context = context;
            _decorated = decorated;
        }

        public void Handle(T command)
        {
            _decorated.Handle(command);
            _context.SaveChanges();
        }
    }
}
