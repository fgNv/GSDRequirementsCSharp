using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.SpecificationItems;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.Links;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class SpecificationItemController : ApiController
    {
        private readonly ICommandHandler<InativateSpecificationItemCommand> _inativateSpecificationItemCommandHandler;
        private readonly IQueryHandler<SpecificationItemIssuesQuery, IEnumerable<IssueViewModel>> _specificationItemIssuesQueryHandler;
        private readonly ICommandHandler<AddSpecificationItemLinkCommand> _addSpecificationItemLinkCommand;
        private readonly ICommandHandler<RemoveSpecificationItemLinkCommand> _removeSpecificationItemLinkCommand;
        private readonly IQueryHandler<LinksFromSpecificationItemQuery, IEnumerable<ItemLinkViewModel>> _linksFromSpecificationItemQueryHandler;

        public SpecificationItemController(ICommandHandler<InativateSpecificationItemCommand> inativateSpecificationItemCommandHandler,
                                           IQueryHandler<SpecificationItemIssuesQuery, IEnumerable<IssueViewModel>> specificationItemIssuesQueryHandler,
                                           ICommandHandler<AddSpecificationItemLinkCommand> addSpecificationItemLinkCommand,
                                           ICommandHandler<RemoveSpecificationItemLinkCommand> removeSpecificationItemLinkCommand,
                                           IQueryHandler<LinksFromSpecificationItemQuery, IEnumerable<ItemLinkViewModel>> linksFromSpecificationItemQueryHandler)
        {
            _inativateSpecificationItemCommandHandler = inativateSpecificationItemCommandHandler;
            _specificationItemIssuesQueryHandler = specificationItemIssuesQueryHandler;
            _addSpecificationItemLinkCommand = addSpecificationItemLinkCommand;
            _removeSpecificationItemLinkCommand = removeSpecificationItemLinkCommand;
            _linksFromSpecificationItemQueryHandler = linksFromSpecificationItemQueryHandler;
        }

        [Route("api/specificationItem/{id}/issues")]
        public IEnumerable<IssueViewModel> GetIssues(Guid id)
        {
            return _specificationItemIssuesQueryHandler.Handle(id);
        }

        [Route("api/specificationItem/{id}/link")]
        [HttpPost]
        public void PostLink(AddSpecificationItemLinkCommand command)
        {
            _addSpecificationItemLinkCommand.Handle(command);
        }

        [Route("api/specificationItem/{id}/link")]
        [HttpDelete]
        public void DeleteLink([FromUri]RemoveSpecificationItemLinkCommand command)
        {
            _removeSpecificationItemLinkCommand.Handle(command);
        }

        [Route("api/specificationItem/{id}/link")]
        [HttpGet]
        public IEnumerable<ItemLinkViewModel> GetLinks(Guid id)
        {
            return _linksFromSpecificationItemQueryHandler.Handle(id);
        }

        // DELETE api/<controller>/5
        public void Delete([FromUri]InativateSpecificationItemCommand command)
        {
            _inativateSpecificationItemCommandHandler.Handle(command);
        }
    }
}