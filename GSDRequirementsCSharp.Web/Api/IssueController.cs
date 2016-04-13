using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Issues;
using GSDRequirementsCSharp.Domain.Commands.IssuesComments;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.IsssueComments;
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
        private ICommandHandler<CreateIssueCommentCommand> _createIssueCommentCommandHandler;
        private IQueryHandler<IssueCommentsQuery, IEnumerable<IssueCommentViewModel>> _issueCommentsQueryHandler;

        public IssueController(ICommandHandler<CreateIssueCommand> createIssueCommandHandler,
                               ICommandHandler<CreateIssueCommentCommand> createIssueCommentCommandHandler,
                               IQueryHandler<IssueCommentsQuery, IEnumerable<IssueCommentViewModel>> issueCommentsQueryHandler)
        {
            _createIssueCommandHandler = createIssueCommandHandler;
            _createIssueCommentCommandHandler = createIssueCommentCommandHandler;
            _issueCommentsQueryHandler = issueCommentsQueryHandler;
        }

        // POST api/<controller>
        public void Post(CreateIssueCommand command)
        {
            _createIssueCommandHandler.Handle(command);
        }

        [Route("api/issue/{issueId}/comment")]
        [HttpGet]
        public IEnumerable<IssueCommentViewModel> GetComments(Guid issueId)
        {
            return _issueCommentsQueryHandler.Handle(issueId);
        }

        [Route("api/issue/{issueId}/comment")]
        [HttpPost]
        public void PostComment(CreateIssueCommentCommand command)
        {
            _createIssueCommentCommandHandler.Handle(command);
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