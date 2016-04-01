using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class AddProjectTranslationCommandCommandHandler : ICommandHandler<AddProjectTranslationCommand>
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<ProjectContent, LocaleKey> _projectContentRepository;

        public AddProjectTranslationCommandCommandHandler(IRepository<Project, Guid> projectRepository,
                                                          IRepository<ProjectContent, LocaleKey> projectContentRepository)
        {
            _projectRepository = projectRepository;
            _projectContentRepository = projectContentRepository;
        }

        public void Handle(AddProjectTranslationCommand command)
        {
            var project = _projectRepository.Get(command.ProjectId);
            var content = new ProjectContent();
            content.Description = command.Description;
            content.Project = project;
            var currentLocale = Thread.CurrentThread.CurrentCulture.Name;
            content.Locale = currentLocale;
            content.Id = project.Id;
            _projectContentRepository.Add(content);
        }
    }
}
