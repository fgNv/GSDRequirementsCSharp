using GSDRequirementsCSharp.Domain.Commands.Projects;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using GSDRequirementsCSharp.Persistence.Commands.Projects;
using GSDRequirementsCSharp.Persistence.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class ProjectController : ApiController
    {
        private readonly IQueryHandler<ProjectsPaginatedQuery, ProjectsPaginatedQueryResult> _projectsPaginatedQueryHandler;
        private readonly ICommandHandler<SaveProjectCommand> _createProjectCommand;
        private readonly ICommandHandler<UpdateProjectCommand> _updateProjectCommand;

        public ProjectController(IQueryHandler<ProjectsPaginatedQuery, ProjectsPaginatedQueryResult> projectsPaginatedQueryHandler,
                                 ICommandHandler<SaveProjectCommand> createProjectCommand,
                                 ICommandHandler<UpdateProjectCommand> updateProjectCommand)
        {
            _projectsPaginatedQueryHandler = projectsPaginatedQueryHandler;
            _createProjectCommand = createProjectCommand;
            _updateProjectCommand = updateProjectCommand;
        }

        public ProjectsPaginatedQueryResult Get([FromUri]ProjectsPaginatedQuery query)
        {
            return _projectsPaginatedQueryHandler.Handle(query);
        }

        public void Post(SaveProjectCommand command)
        {
            _createProjectCommand.Handle(command);
        }

        public void Put(Guid id, SaveProjectCommand command)
        {
            var updateCommand = new UpdateProjectCommand();
            updateCommand.Id = id;
            updateCommand.Name = command.Name;
            updateCommand.Description = command.Description;
            _updateProjectCommand.Handle(updateCommand);
        }
    }
}
