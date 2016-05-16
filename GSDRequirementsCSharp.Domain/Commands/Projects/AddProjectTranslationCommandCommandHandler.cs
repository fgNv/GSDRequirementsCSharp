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
        private readonly IQueryHandler<Guid, Project> _projectWithContentsQueryHandler;
        private readonly IRepository<ProjectContent, LocaleKey> _projectContentRepository;

        public AddProjectTranslationCommandCommandHandler(IQueryHandler<Guid, Project> projectWithContentsQueryHandler,
                                                          IRepository<ProjectContent, LocaleKey> projectContentRepository)
        {
            _projectWithContentsQueryHandler = projectWithContentsQueryHandler;
            _projectContentRepository = projectContentRepository;
        }

        public void Handle(AddProjectTranslationCommand command)
        {
            var project = _projectWithContentsQueryHandler.Handle(command.Id.Value);
            foreach (var item in command.Items)
            {
                var content = project.ProjectContents.FirstOrDefault(c => c.Locale == item.Locale);

                if (content != null && content.IsUpdated)
                    continue;
                
                if(content == null)
                {
                    content = new ProjectContent();
                    _projectContentRepository.Add(content);
                    content.Project = project;
                    content.Id = project.Id;
                    content.Locale = item.Locale;
                }

                content.IsUpdated = true;
                content.Name = item.Name;
                content.Description = item.Description;
            }
        }
    }
}
