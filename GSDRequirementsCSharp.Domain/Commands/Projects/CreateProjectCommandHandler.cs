using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Projects;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Projects;
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
    class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<ProjectContent, LocaleKey> _projectContentRepository;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IQueryHandler<ProjectNextIdQuery, int> _projectNextIdQuery;
        private readonly IRepository<Permission, Guid> _permissionRepository;

        public CreateProjectCommandHandler(IRepository<Project, Guid> projectRepository,
                                           IRepository<ProjectContent, LocaleKey> projectContentRepository,
                                           ICurrentUserRetriever<User> currentUserRetriever,
                                           IQueryHandler<ProjectNextIdQuery, int> projectNextIdQuery,
                                           IRepository<Permission, Guid> permissionRepository)
        {
            _projectRepository = projectRepository;
            _projectContentRepository = projectContentRepository;
            _currentUserRetriever = currentUserRetriever;
            _projectNextIdQuery = projectNextIdQuery;
            _permissionRepository = permissionRepository;
        }

        public void Handle(CreateProjectCommand command)
        {
            var project = new Project();
            project.Id = Guid.NewGuid();
            project.Active = true;

            foreach (var item in command.Items)
            {
                var content = new ProjectContent();
                content.Description = item.Description;
                content.Name = item.Name;
                content.Locale = item.Locale;
                content.Project = project;
                content.IsUpdated = true;
                content.Id = project.Id;
                _projectContentRepository.Add(content);
            }

            project.CreatedAt = DateTime.Now;
            var currentUser = _currentUserRetriever.Get();

            if (currentUser == null)
                throw new Exception(Sentences.youMustBeLoggedIn);

            var identifier = _projectNextIdQuery.Handle(currentUser.Id);

            project.Owner = currentUser;
            project.CreatorId = currentUser.Id;
            project.Identifier = identifier;

            var permission = new Permission();
            permission.Id = Guid.NewGuid();
            permission.Profile = Profile.ProjectOwner;
            permission.Project = project;
            permission.User = currentUser;

            _permissionRepository.Add(permission);
            _projectRepository.Add(project);
        }
    }
}
