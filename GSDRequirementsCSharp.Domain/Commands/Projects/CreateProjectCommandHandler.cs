using GSDRequirementsCSharp.Domain;
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
    class CreateProjectCommandHandler : ICommandHandler<SaveProjectCommand>
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

        public void Handle(SaveProjectCommand command)
        {
            var project = new Project();
            project.Id = Guid.NewGuid();
            project.Name = command.Name;
            var content = new ProjectContent();
            content.Description = command.Description;
            content.Locale = Thread.CurrentThread.CurrentCulture.Name; ;
            content.Project = project;
            content.Id = Guid.NewGuid();
            project.CreatedAt = DateTime.Now;
            var currentUser = _currentUserRetriever.Get();
            project.OwnerId = currentUser.Id;

            _projectRepository.Add(project);
            _projectContentRepository.Add(content);
        }
    }
}
