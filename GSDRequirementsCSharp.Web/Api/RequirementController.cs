using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Converter;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Validation;
using GSDRequirementsCSharp.Persistence.Queries;
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
        private readonly IConverter<Requirement, CreateRequirementVersionCommand> _requirementToNewVersionCommand;
        private readonly IValidator _validator;
        private readonly ICommandHandler<RemoveRequirementCommand> _removeRequirementCommandHandler;

        public RequirementController(IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> requirementsPaginatedQueryHandler,
                                     ICommandHandler<SaveRequirementCommand> createRequirementCommandHandler,
                                     ICommandHandler<CreateRequirementVersionCommand> createRequirementVersionCommandHandler,
                                     ICommandHandler<AddRequirementTranslationCommand> addRequirementTranslationCommandHandler,
                                     IQueryHandler<DetailedRequirementQuery, Requirement> detailedRequirementQueryHandler,
                                     IQueryHandler<RequirementVersionsQuery, IEnumerable<VersionItem>> requirementVersionsQuery,
                                     IConverter<Requirement, CreateRequirementVersionCommand> requirementToNewVersionCommand,
                                     IValidator validator,
                                     ICommandHandler<RemoveRequirementCommand> removeRequirementCommandHandler)
        {
            _requirementsPaginatedQueryHandler = requirementsPaginatedQueryHandler;
            _createRequirementCommand = createRequirementCommandHandler;
            _createRequirementVersionCommandHandler = createRequirementVersionCommandHandler;
            _addRequirementTranslationCommandHandler = addRequirementTranslationCommandHandler;
            _detailedRequirementQueryHandler = detailedRequirementQueryHandler;
            _requirementVersionsQuery = requirementVersionsQuery;
            _requirementToNewVersionCommand = requirementToNewVersionCommand;
            _removeRequirementCommandHandler = removeRequirementCommandHandler;
            _validator = validator;
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

        [HttpPost]
        [Route("api/requirement/{id}/versions")]
        public void Versions(RestoreVersionCommand command)
        {
            _validator.Validate(command);
            var useCaseDiagram = _detailedRequirementQueryHandler.Handle(command);
            var newVersionCommand = _requirementToNewVersionCommand.Convert(useCaseDiagram);
            _createRequirementVersionCommandHandler.Handle(newVersionCommand);
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

        public void Delete(RemoveRequirementCommand command)
        {
            _removeRequirementCommandHandler.Handle(command);
        }
    }
}