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
        private readonly IQueryHandler<Guid, ProjectWithCurrentCultureContentsQueryResult> 
            _projectWithCurrentCultureContentsQueryHandler;

        public UpdateProjectCommandHandler(IQueryHandler<Guid, ProjectWithCurrentCultureContentsQueryResult> projectWithCurrentCultureContentsQueryHandler)
        {
            _projectWithCurrentCultureContentsQueryHandler = 
                projectWithCurrentCultureContentsQueryHandler;
        }

        public void Handle(UpdateProjectCommand command)
        {
            var queryResult = 
                _projectWithCurrentCultureContentsQueryHandler.Handle(command.Id);
            queryResult.Project.Name = command.Name;
            queryResult.ProjectContent.Description = command.Description;
        }
    }
}
