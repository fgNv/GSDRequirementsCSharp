using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class InactivateProjectCommandHandler : ICommandHandler<InactivateProjectCommand>
    {
        private readonly IRepository<Project, Guid> _projectRepository;

        public InactivateProjectCommandHandler(IRepository<Project, Guid> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Handle(InactivateProjectCommand command)
        {
            var project = _projectRepository.Get(command.Id.Value);
            project.Active = false;
        }
    }
}
