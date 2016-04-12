using GSDRequirementsCSharp.Domain.Commands.Issues;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class IssueController : ApiController
    {
        private ICommandHandler<CreateIssueCommand> _createIssueCommandHandler;

        public IssueController(ICommandHandler<CreateIssueCommand> createIssueCommandHandler)
        {
            _createIssueCommandHandler = createIssueCommandHandler;
        }

        // POST api/<controller>
        public void Post(CreateIssueCommand command)
        {
            _createIssueCommandHandler.Handle(command);
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