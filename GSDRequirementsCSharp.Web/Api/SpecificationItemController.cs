using GSDRequirementsCSharp.Domain.Commands.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class SpecificationItemController : ApiController
    {
        private readonly ICommandHandler<InativateSpecificationItemCommand> _inativateSpecificationItemCommandHandler;

        public SpecificationItemController(ICommandHandler<InativateSpecificationItemCommand> inativateSpecificationItemCommandHandler)
        {
            _inativateSpecificationItemCommandHandler = inativateSpecificationItemCommandHandler;
        }

        // DELETE api/<controller>/5
        public void Delete([FromUri]InativateSpecificationItemCommand command)
        {
            _inativateSpecificationItemCommandHandler.Handle(command);
        }
    }
}