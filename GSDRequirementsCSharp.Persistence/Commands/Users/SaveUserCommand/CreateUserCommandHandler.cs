using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand
{
    public class CreateUserCommandHandler : GenericCreateCommandHandler<CreateUserCommand, User, Guid>
    {
        public CreateUserCommandHandler(ICommandToModelConverter<CreateUserCommand, User> commandToModel,
                                        IRepository<User, Guid> repository)
            : base(commandToModel, repository) { }
    }
}
