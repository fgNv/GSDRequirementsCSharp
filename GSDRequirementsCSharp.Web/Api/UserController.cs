﻿using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Commands;
using GSDRequirementsCSharp.Persistence.Queries.Users.BySearchTerm;
using GSDRequirementsCSharp.Web.Api.Globals;
using GSDRequirementsCSharp.Web.Context;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class UserController : ApiController
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly IQueryHandler<UsersBySearchTermQuery, IEnumerable<UserViewModel>> _userBySearchTermQueryHandler;
        private readonly ICommandHandler<UpdateUserCommand> _updateUserCommandHandler;
        private readonly ICommandHandler<ChangeUserPasswordCommand> _changeUserPasswordCommandHandler;

        public UserController(ICommandHandler<CreateUserCommand> createUserCommandHandler,
                              IQueryHandler<UsersBySearchTermQuery, IEnumerable<UserViewModel>> userBySearchTermQueryHandler,
                              ICommandHandler<UpdateUserCommand> updateUserCommand,
                              ICommandHandler<ChangeUserPasswordCommand> changeUserPasswordCommandHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _userBySearchTermQueryHandler = userBySearchTermQueryHandler;
            _updateUserCommandHandler = updateUserCommand;
            _changeUserPasswordCommandHandler = changeUserPasswordCommandHandler;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]CreateUserCommand command)
        {
            var response = new HttpResponseMessage();
            var projectCookieRemoval = CookieRemoval.Get(ActionContext.Request, ProjectContext.COOKIE_NAME);
            if (projectCookieRemoval != null)
                response.Headers.AddCookies(new[] { projectCookieRemoval });

            _createUserCommandHandler.Handle(command);
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }

        // PUT api/<controller>/5
        public void Put(UpdateUserCommand command)
        {
            _updateUserCommandHandler.Handle(command);
        }

        // PUT api/<controller>/5
        [Route("api/user/password")]
        public void Put(ChangeUserPasswordCommand command)
        {
            _changeUserPasswordCommandHandler.Handle(command);
        }

        public IEnumerable<UserViewModel> Get([FromUri]UsersBySearchTermQuery query)
        {
            return _userBySearchTermQueryHandler.Handle(query);
        }
    }
}