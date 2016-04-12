using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.SpecificationItems;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class SpecificationItemController : ApiController
    {
        private readonly ICommandHandler<InativateSpecificationItemCommand> _inativateSpecificationItemCommandHandler;
        private readonly IQueryHandler<SpecificationItemIssuesQuery, IEnumerable<IssueViewModel>> _specificationItemIssuesQueryHandler;

        public SpecificationItemController(ICommandHandler<InativateSpecificationItemCommand> inativateSpecificationItemCommandHandler,
                                           IQueryHandler<SpecificationItemIssuesQuery, IEnumerable<IssueViewModel>> specificationItemIssuesQueryHandler)
        {
            _inativateSpecificationItemCommandHandler = inativateSpecificationItemCommandHandler;
            _specificationItemIssuesQueryHandler = specificationItemIssuesQueryHandler;
        }

        [Route("api/specificationItem/{id}/issues")]
        public IEnumerable<IssueViewModel> GetIssues(Guid id)
        {
            return _specificationItemIssuesQueryHandler.Handle(id);
        }

        // DELETE api/<controller>/5
        public void Delete([FromUri]InativateSpecificationItemCommand command)
        {
            _inativateSpecificationItemCommandHandler.Handle(command);
        }
    }
}