﻿using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Projects;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Projects
{
    public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
    {
        private readonly IQueryHandler<Guid, Project> _projectWithContentsQueryHandler;
        private readonly IRepository<ProjectContent, LocaleKey> _projectContentRepository;

        public UpdateProjectCommandHandler(IQueryHandler<Guid, Project> projectWithContentsQueryHandler,
                                           IRepository<ProjectContent, LocaleKey> projectContentRepository)
        {
            _projectWithContentsQueryHandler = projectWithContentsQueryHandler;
            _projectContentRepository = projectContentRepository;
        }

        public void Handle(UpdateProjectCommand command)
        {
            var project = _projectWithContentsQueryHandler.Handle(command.Id);
            foreach (var content in project.ProjectContents)
            {
                if(command.Items.Any(i => content.Locale == i.Locale))
                {
                    var item = command.Items.FirstOrDefault(i => content.Locale == i.Locale);
                    content.IsUpdated = true;
                    content.Name = item.Name;
                    content.Description = item.Description;
                }
                else
                {
                    content.IsUpdated = false;
                }
            }

            foreach(var item in command.Items)
            {
                if (project.ProjectContents.Any(p => p.Locale == item.Locale))
                    continue;

                var content = new ProjectContent();
                content.Id = project.Id;
                content.IsUpdated = true;
                content.Name = item.Name;
                content.Locale = item.Locale;
                content.Description = item.Description;
                content.Project = project;

                _projectContentRepository.Add(content);
            }
        }
    }
}
