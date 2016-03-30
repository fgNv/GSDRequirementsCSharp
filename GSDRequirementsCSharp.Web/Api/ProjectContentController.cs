using GSDRequirementsCSharp.Domain.Commands.Projects;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class ProjectContentController : ApiController
    {
        private readonly ICommandHandler<AddProjectTranslationCommand> _addProjectTranslationCommandHandler;

        public ProjectContentController(ICommandHandler<AddProjectTranslationCommand> addProjectTranslationCommandHandler)
        {
            _addProjectTranslationCommandHandler = addProjectTranslationCommandHandler;
        }
        
        // POST: api/ProjectContent
        public void Post(AddProjectTranslationCommand command)
        {
            _addProjectTranslationCommandHandler.Handle(command);
        }
    }
}
