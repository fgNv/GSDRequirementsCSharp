using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class UserController : ApiController
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;

        public UserController(ICommandHandler<CreateUserCommand> createUserCommandHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
        }

        // POST api/<controller>
        public void Post([FromBody]CreateUserCommand command)
        {
            _createUserCommandHandler.Handle(command);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
        
    }
}