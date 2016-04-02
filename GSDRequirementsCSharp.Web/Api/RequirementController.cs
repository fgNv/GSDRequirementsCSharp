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
        private ICommandHandler<SaveRequirementCommand> _createRequirementCommand;

        public RequirementController(IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult> requirementsPaginatedQueryHandler,
                                     ICommandHandler<SaveRequirementCommand> createRequirementCommand)
        {
            _requirementsPaginatedQueryHandler = requirementsPaginatedQueryHandler;
            _createRequirementCommand = createRequirementCommand;
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}