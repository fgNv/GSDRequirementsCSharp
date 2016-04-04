using GSDRequirementsCSharp.Domain.Commands.Requirements;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class RequirementController : ApiController
    {
        private readonly IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> _requirementsPaginatedQueryHandler;
        private readonly ICommandHandler<SaveRequirementCommand> _createRequirementCommand;
        private readonly ICommandHandler<CreateRequirementVersionCommand> _createRequirementVersionCommandHandler;
        private readonly ICommandHandler<AddRequirementTranslationCommand> _addRequirementTranslationCommandHandler;

        public RequirementController(IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> requirementsPaginatedQueryHandler,
                                     ICommandHandler<SaveRequirementCommand> createRequirementCommandHandler,
                                     ICommandHandler<CreateRequirementVersionCommand> createRequirementVersionCommandHandler,
                                     ICommandHandler<AddRequirementTranslationCommand> addRequirementTranslationCommandHandler)
        {
            _requirementsPaginatedQueryHandler = requirementsPaginatedQueryHandler;
            _createRequirementCommand = createRequirementCommandHandler;
            _createRequirementVersionCommandHandler = createRequirementVersionCommandHandler;
            _addRequirementTranslationCommandHandler = addRequirementTranslationCommandHandler;
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