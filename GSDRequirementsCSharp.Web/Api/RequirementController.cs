using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Requirements;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using System;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class RequirementController : ApiController
    {
        private readonly IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> _requirementsPaginatedQueryHandler;
        private readonly ICommandHandler<SaveRequirementCommand> _createRequirementCommand;
        private readonly ICommandHandler<CreateRequirementVersionCommand> _createRequirementVersionCommandHandler;
        private readonly ICommandHandler<AddRequirementTranslationCommand> _addRequirementTranslationCommandHandler;
        private readonly IQueryHandler<LastVersionRequirementQuery, Requirement> _requirementLastVersionQueryHandler;

        public RequirementController(IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> requirementsPaginatedQueryHandler,
                                     ICommandHandler<SaveRequirementCommand> createRequirementCommandHandler,
                                     ICommandHandler<CreateRequirementVersionCommand> createRequirementVersionCommandHandler,
                                     ICommandHandler<AddRequirementTranslationCommand> addRequirementTranslationCommandHandler,
                                     IQueryHandler<LastVersionRequirementQuery, Requirement> requirementLastVersionQueryHandler)
        {
            _requirementsPaginatedQueryHandler = requirementsPaginatedQueryHandler;
            _createRequirementCommand = createRequirementCommandHandler;
            _createRequirementVersionCommandHandler = createRequirementVersionCommandHandler;
            _addRequirementTranslationCommandHandler = addRequirementTranslationCommandHandler;
            _requirementLastVersionQueryHandler = requirementLastVersionQueryHandler;
        }

        public RequirementViewModel Get(Guid id)
        {
            var requirement = _requirementLastVersionQueryHandler.Handle(id);
            return RequirementViewModel.FromModel(requirement);
        }
        
        [Route("api/requirement/{page}/{pageSize}")]
        public RequirementsPaginatedQueryResult Get([FromUri]RequirementsPaginatedQuery query)
        {
            return _requirementsPaginatedQueryHandler.Handle(query);
        }
        
        // POST api/<controller>
        public void Post(SaveRequirementCommand command)
        {
            _createRequirementCommand.Handle(command);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]CreateRequirementVersionCommand command)
        {
            _createRequirementVersionCommandHandler.Handle(command);
        }

        [Route("api/requirement/{id}/translation")]
        public void Post(AddRequirementTranslationCommand command)
        {
            _addRequirementTranslationCommandHandler.Handle(command);
        }
    }
}