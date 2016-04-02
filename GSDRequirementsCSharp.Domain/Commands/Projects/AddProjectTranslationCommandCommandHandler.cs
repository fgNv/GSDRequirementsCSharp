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

        public AddProjectTranslationCommandCommandHandler(IQueryHandler<Guid, Project> projectWithContentsQueryHandler)
        {
            _projectWithContentsQueryHandler = projectWithContentsQueryHandler;
        }

        public void Handle(AddProjectTranslationCommand command)
        {
            var project = _projectWithContentsQueryHandler.Handle(command.Id);
            foreach (var content in project.ProjectContents)
            {
                if (content.IsUpdated)
                    continue;

                var item = command.Items.FirstOrDefault(i => content.Locale == i.Locale);
                if (item == null)
                    continue;

                content.IsUpdated = true;
                content.Name = item.Name;
                content.Description = item.Description;
            }
        }
    }
}
