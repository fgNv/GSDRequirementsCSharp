using GSDRequirementsCSharp.Domain.Commands.Projects;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.ByProject;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class ProjectController : ApiController
    {
        private readonly IQueryHandler<ProjectsPaginatedQuery, ProjectsPaginatedQueryResult> _projectsPaginatedQueryHandler;
        private readonly ICommandHandler<CreateProjectCommand> _createProjectCommand;
        private readonly ICommandHandler<UpdateProjectCommand> _updateProjectCommand;
        private readonly IQueryHandler<CurrentUserProjectsQuery, CurrentUserProjectsQueryResult> _currentUserProjectsQueryHandler;
        private readonly ICommandHandler<InactivateProjectCommand> _inactivateProjectCommand;
        private readonly ICommandHandler<AddProjectTranslationCommand> _addProjectTranslationCommandHandler;
        private readonly IQueryHandler<ItemsByProjectQuery, IEnumerable<SpecificationItemViewModel>> _itemsByProjectQueryHandler;
        private readonly ICurrentProjectContextId _projectContextId;

        public ProjectController(IQueryHandler<ProjectsPaginatedQuery, ProjectsPaginatedQueryResult> projectsPaginatedQueryHandler,
                                 ICommandHandler<CreateProjectCommand> createProjectCommand,
                                 ICommandHandler<UpdateProjectCommand> updateProjectCommand,
                                 IQueryHandler<CurrentUserProjectsQuery, CurrentUserProjectsQueryResult> currentUserProjectsQueryHandler,
                                 ICommandHandler<InactivateProjectCommand> inactivateProjectCommand,
                                 ICommandHandler<AddProjectTranslationCommand> addProjectTranslationCommandHandler,
                                 IQueryHandler<ItemsByProjectQuery, IEnumerable<SpecificationItemViewModel>> itemsByProjectQueryHandler,
                                 ICurrentProjectContextId projectContext)
        {
            _projectsPaginatedQueryHandler = projectsPaginatedQueryHandler;
            _createProjectCommand = createProjectCommand;
            _updateProjectCommand = updateProjectCommand;
            _currentUserProjectsQueryHandler = currentUserProjectsQueryHandler;
            _inactivateProjectCommand = inactivateProjectCommand;
            _addProjectTranslationCommandHandler = addProjectTranslationCommandHandler;
            _itemsByProjectQueryHandler = itemsByProjectQueryHandler;
            _projectContextId = projectContext;
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

        public void Post(CreateProjectCommand command)
        {
            _createProjectCommand.Handle(command);
        }

        public void Put(UpdateProjectCommand command)
        {
            _updateProjectCommand.Handle(command);
        }

        public void Delete(Guid id)
        {
            _inactivateProjectCommand.Handle(id);
        }
        
        [Route("api/project/{id}/translation")]
        public void AddTranslations(AddProjectTranslationCommand command)
        {
            _addProjectTranslationCommandHandler.Handle(command);
        }

        [HttpGet]
        [Route("api/project/{projectId}/specificationItem")]
        public IEnumerable<SpecificationItemViewModel> SpecificationItems(Guid projectId)
        {
            return _itemsByProjectQueryHandler.Handle(projectId);
        }

        [HttpGet]
        [Route("api/currentProject/specificationItem")]
        public IEnumerable<SpecificationItemViewModel> SpecificationItems()
        {
            var projectId = _projectContextId.Get();
            return _itemsByProjectQueryHandler.Handle(projectId);
        }
    }
}
