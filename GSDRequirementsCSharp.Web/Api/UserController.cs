using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand;
using GSDRequirementsCSharp.Persistence.Queries.Users.BySearchTerm;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class UserController : ApiController
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly IQueryHandler<UsersBySearchTermQuery, IEnumerable<UserViewModel>> _userBySearchTermQueryHandler;

        public UserController(ICommandHandler<CreateUserCommand> createUserCommandHandler,
                              IQueryHandler<UsersBySearchTermQuery, IEnumerable<UserViewModel>> userBySearchTermQueryHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _userBySearchTermQueryHandler = userBySearchTermQueryHandler;
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
        
        public IEnumerable<UserViewModel> Get([FromUri]UsersBySearchTermQuery query)
        {
            return _userBySearchTermQueryHandler.Handle(query);
        }
    }
}