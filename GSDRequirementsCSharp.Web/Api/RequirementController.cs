using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Requirements;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class RequirementController : ApiController
    {
        private readonly IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> _requirementsPaginatedQueryHandler;
        private readonly ICommandHandler<SaveRequirementCommand> _createRequirementCommand;
        private readonly ICommandHandler<CreateRequirementVersionCommand> _createRequirementVersionCommandHandler;
        private readonly ICommandHandler<AddRequirementTranslationCommand> _addRequirementTranslationCommandHandler;
        private readonly IQueryHandler<DetailedRequirementQuery, Requirement> _detailedRequirementQueryHandler;
        private readonly IQueryHandler<RequirementVersionsQuery, IEnumerable<VersionItem>> _requirementVersionsQuery;

        public RequirementController(IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> requirementsPaginatedQueryHandler,
                                     ICommandHandler<SaveRequirementCommand> createRequirementCommandHandler,
                                     ICommandHandler<CreateRequirementVersionCommand> createRequirementVersionCommandHandler,
                                     ICommandHandler<AddRequirementTranslationCommand> addRequirementTranslationCommandHandler,
                                     IQueryHandler<DetailedRequirementQuery, Requirement> detailedRequirementQueryHandler,
                                     IQueryHandler<RequirementVersionsQuery, IEnumerable<VersionItem>> requirementVersionsQuery)
        {
            _requirementsPaginatedQueryHandler = requirementsPaginatedQueryHandler;
            _createRequirementCommand = createRequirementCommandHandler;
            _createRequirementVersionCommandHandler = createRequirementVersionCommandHandler;
            _addRequirementTranslationCommandHandler = addRequirementTranslationCommandHandler;
            _detailedRequirementQueryHandler = detailedRequirementQueryHandler;
            _requirementVersionsQuery = requirementVersionsQuery;
        }

        public RequirementViewModel Get([FromUri] DetailedRequirementQuery query)
        {
            var requirement = _detailedRequirementQueryHandler.Handle(query);
            return RequirementViewModel.FromModel(requirement);
        }
        
        [Route("api/requirement/{page}/{pageSize}")]
        public RequirementsPaginatedQueryResult Get([FromUri]RequirementsPaginatedQuery query)
        {
            return _requirementsPaginatedQueryHandler.Handle(query);
        }
        
        [HttpGet]
        [Route("api/requirement/{id}/versions")]
        public IEnumerable<VersionItem> Versions([FromUri]RequirementVersionsQuery query)
        {
            var result = _requirementVersionsQuery.Handle(query);
            return result;
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