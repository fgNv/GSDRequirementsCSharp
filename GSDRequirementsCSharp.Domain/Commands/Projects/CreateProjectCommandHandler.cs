using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
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
        private readonly IRepository<ProjectContent, LocaleKey> _projectContentRepository;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public CreateProjectCommandHandler(IRepository<Project, Guid> projectRepository,
                                           IRepository<ProjectContent, LocaleKey> projectContentRepository,
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
            project.Active = true;

            foreach (var item in command.Items)
            {
                var content = new ProjectContent();
                content.Description = item.Description;
                content.Name = item.Name;
                content.Locale = Thread.CurrentThread.CurrentCulture.Name; ;
                content.Project = project;
                content.IsUpdated = true;
                content.Id = project.Id;
                _projectContentRepository.Add(content);
            }

            project.CreatedAt = DateTime.Now;
            var currentUser = _currentUserRetriever.Get();

            if (currentUser == null)
                throw new Exception(Sentences.youMustBeLoggedIn);

            project.Owner = currentUser;

            _projectRepository.Add(project);
        }
    }
}
