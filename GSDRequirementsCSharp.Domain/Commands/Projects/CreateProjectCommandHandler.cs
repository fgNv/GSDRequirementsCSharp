using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Commands.Projects
{
    class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<ProjectContent, Guid> _projectContentRepository;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public CreateProjectCommandHandler(IRepository<Project, Guid> projectRepository,
                                           IRepository<ProjectContent, Guid> projectContentRepository,
                                           ICurrentUserRetriever<User> currentUserRetriever)
        {
            _projectRepository = projectRepository;
            _projectContentRepository = projectContentRepository;
            _currentUserRetriever = currentUserRetriever;
        }

        public void Handle(CreateProjectCommand command)
        {
            var project = new Project();
            project.id = Guid.NewGuid();
            project.name = command.Name;
            var content = new ProjectContent();
            content.description = command.Description;
            content.locale = Thread.CurrentThread.CurrentCulture.Name; ;
            content.Project = project;
            content.id = Guid.NewGuid();
            project.created_at = DateTime.Now;
            var currentUser = _currentUserRetriever.Get();
            project.owner_id = currentUser.id;

            _projectRepository.Add(project);
            _projectContentRepository.Add(content);
        }
    }
}
