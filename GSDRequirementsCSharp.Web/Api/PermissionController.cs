using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Permissions;
using GSDRequirementsCSharp.Domain.Queries.Permissions;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class PermissionController : ApiController
    {
        private readonly ICommandHandler<SavePermissionCommand> _savePermissionCommandHandler;
        private readonly IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>> _permissionsByCurrentProjectQueryHandler;

        public PermissionController(IQueryHandler<PermissionsByCurrentProjectQuery, IEnumerable<Permission>> permissionsByCurrentProjectQueryHandler,
                                    ICommandHandler<SavePermissionCommand> savePermissionCommandHandler)
        {
            _permissionsByCurrentProjectQueryHandler = permissionsByCurrentProjectQueryHandler;
            _savePermissionCommandHandler = savePermissionCommandHandler;
        }

        public IEnumerable<Permission> Get()
        {
            var result = _permissionsByCurrentProjectQueryHandler.Handle(null);
            return result;
        }
        
        // POST api/<controller>
        public void Post([FromBody]SavePermissionCommand command)
        {
            _savePermissionCommandHandler.Handle(command);
        }        
    }
}