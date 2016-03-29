using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Projects
{
    class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public CreateProjectCommandHandler(GSDRequirementsContext context,
                                           ICurrentUserRetriever<User> currentUserRetriever)
        {
            _context = context;
            _currentUserRetriever = currentUserRetriever;
        }

        public void Handle(CreateProjectCommand command)
        {
            var project = new Project();
            project.id = Guid.NewGuid();
            project.name = command.Name;
            var content = new ProjectContent();
            content.description = command.Description;
            content.locale = command.Locale;
            content.Project = project;
            content.id = Guid.NewGuid();
            project.created_at = DateTime.Now;
            var currentUser = _currentUserRetriever.Get();
            project.owner_id = currentUser.id;

            _context.Projects.Add(project);
            _context.ProjectContents.Add(content);
        }
    }
}
