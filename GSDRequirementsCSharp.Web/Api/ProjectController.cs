using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Projects;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using GSDRequirementsCSharp.Persistence.Commands.Projects;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.Projects.CurrentUserProjects;
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
        private readonly IQueryHandler<CurrentUserProjectsQuery, CurrentUserProjectsQueryResult> _currentUserProjectsQueryHandler;

        public ProjectController(IQueryHandler<ProjectsPaginatedQuery, ProjectsPaginatedQueryResult> projectsPaginatedQueryHandler,
                                 ICommandHandler<SaveProjectCommand> createProjectCommand,
                                 ICommandHandler<UpdateProjectCommand> updateProjectCommand,
                                 IQueryHandler<CurrentUserProjectsQuery, CurrentUserProjectsQueryResult> currentUserProjectsQueryHandler)
        {
            _projectsPaginatedQueryHandler = projectsPaginatedQueryHandler;
            _createProjectCommand = createProjectCommand;
            _updateProjectCommand = updateProjectCommand;
            _currentUserProjectsQueryHandler = currentUserProjectsQueryHandler;
        }

        [Route("api/currentUser/projects")]
        public IEnumerable<ProjectOption> Get()
        {
            var result = _currentUserProjectsQueryHandler.Handle(null);
            return result.Projects;
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
            var updateCommand = new UpdateProjectCommand(id, command);
            _updateProjectCommand.Handle(updateCommand);
        }
    }
}
