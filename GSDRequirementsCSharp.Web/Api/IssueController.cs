using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.IsssueComments;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class IssueController : ApiController
    {
        private readonly ICommandHandler<CreateIssueCommand> _createIssueCommandHandler;
        private readonly ICommandHandler<CreateIssueCommentCommand> _createIssueCommentCommandHandler;
        private readonly IQueryHandler<IssueCommentsQuery, IEnumerable<IssueCommentViewModel>> _issueCommentsQueryHandler;
        private readonly ICommandHandler<ConcludeIssueCommand> _concludeIssueCommandHandler;

        public IssueController(ICommandHandler<CreateIssueCommand> createIssueCommandHandler,
                               ICommandHandler<CreateIssueCommentCommand> createIssueCommentCommandHandler,
                               IQueryHandler<IssueCommentsQuery, IEnumerable<IssueCommentViewModel>> issueCommentsQueryHandler,
                               ICommandHandler<ConcludeIssueCommand> concludeIssueCommandHandler)
        {
            _createIssueCommandHandler = createIssueCommandHandler;
            _createIssueCommentCommandHandler = createIssueCommentCommandHandler;
            _issueCommentsQueryHandler = issueCommentsQueryHandler;
            _concludeIssueCommandHandler = concludeIssueCommandHandler;
        }

        [Route("api/issue/{issueId}/conclude")]
        [HttpPut]
        public void Conclude([FromUri]ConcludeIssueCommand command)
        {
            _concludeIssueCommandHandler.Handle(command);
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