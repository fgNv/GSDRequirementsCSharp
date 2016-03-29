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
        private readonly ICommandHandler<CreateProjectCommand> _createProjectCommand;

        public ProjectController(IQueryHandler<ProjectsPaginatedQuery, ProjectsPaginatedQueryResult> projectsPaginatedQueryHandler,
                                 ICommandHandler<CreateProjectCommand> createProjectCommand)
        {
            _projectsPaginatedQueryHandler = projectsPaginatedQueryHandler;
            _createProjectCommand = createProjectCommand;
        }

        public ProjectsPaginatedQueryResult Get([FromUri]ProjectsPaginatedQuery query)
        {
            return _projectsPaginatedQueryHandler.Handle(query);
        }

        public void Post(CreateProjectCommand command)
        {
            _createProjectCommand.Handle(command);
        }
    }
}
