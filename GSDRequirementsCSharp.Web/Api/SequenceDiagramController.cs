using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class SequenceDiagramController : ApiController
    {
        private readonly ICommandHandler<SaveSequenceDiagramCommand> _saveSequenceDiagramCommandHandler;

        public SequenceDiagramController(ICommandHandler<SaveSequenceDiagramCommand> saveSequenceDiagramCommandHandler)
        {
            _saveSequenceDiagramCommandHandler = saveSequenceDiagramCommandHandler;
        }

        public void Post(SaveSequenceDiagramCommand request)
        {
            var generatedId = Guid.NewGuid();
            request.SequenceDiagramId = generatedId;
            _saveSequenceDiagramCommandHandler.Handle(request);
        }
    }
}