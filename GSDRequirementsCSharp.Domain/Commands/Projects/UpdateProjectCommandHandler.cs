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

        public UpdateProjectCommandHandler(IQueryHandler<Guid, Project> projectWithContentsQueryHandler)
        {
            _projectWithContentsQueryHandler = projectWithContentsQueryHandler;
        }

        public void Handle(UpdateProjectCommand command)
        {
            var project = _projectWithContentsQueryHandler.Handle(command.Id);
            foreach (var content in project.ProjectContents)
            {
                if(command.Items.Any(i => content.Locale == i.LocaleName))
                {
                    var item = command.Items.FirstOrDefault(i => content.Locale == i.LocaleName);
                    content.IsUpdated = true;
                    content.Name = item.Name;
                    content.Description = item.Description;
                }
                else
                {
                    content.IsUpdated = false;
                }
            }
        }
    }
}
