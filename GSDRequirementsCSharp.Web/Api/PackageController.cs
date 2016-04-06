using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands.Packages;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.Packages.CurrentProject;
using GSDRequirementsCSharp.Persistence.Queries.Packages.Paginated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class PackageController : ApiController
    {
        private readonly IQueryHandler<PackagesPaginatedQuery, PackagesPaginatedQueryResult> _packagesPaginatedQueryHandler;
        private readonly ICommandHandler<AddPackageTranslationCommand> _addPackageTranslationCommandHandler;
        private readonly ICommandHandler<SavePackageCommand> _createPackageCommandHandler;
        private readonly ICommandHandler<UpdatePackageCommand> _updatePackageCommandHandler;
        private readonly ICommandHandler<InactivatePackageCommand> _inactivatePackageCommand;
        private readonly IQueryHandler<PackagesCurrentProjectQuery, IEnumerable<Package>> _packagesCurrentProjectQueryHandler;

        public PackageController(IQueryHandler<PackagesPaginatedQuery, PackagesPaginatedQueryResult> packagesPaginatedQueryHandler,
                                ICommandHandler<AddPackageTranslationCommand> addPackageTranslationCommandHandler,
                                ICommandHandler<SavePackageCommand> createPackageCommandHandler,
                                ICommandHandler<UpdatePackageCommand> updatePackageCommandHandler,
                                ICommandHandler<InactivatePackageCommand> inactivatePackageCommand,
                                IQueryHandler<PackagesCurrentProjectQuery, IEnumerable<Package>> packagesCurrentProjectQueryHandler)
        {
            _packagesPaginatedQueryHandler = packagesPaginatedQueryHandler;
            _addPackageTranslationCommandHandler = addPackageTranslationCommandHandler;
            _createPackageCommandHandler = createPackageCommandHandler;
            _updatePackageCommandHandler = updatePackageCommandHandler;
            _inactivatePackageCommand = inactivatePackageCommand;
            _packagesCurrentProjectQueryHandler = packagesCurrentProjectQueryHandler;
        }
        
        public IEnumerable<Package> Get()
        {
            return _packagesCurrentProjectQueryHandler.Handle(null);
        }

        [Route("api/package/{page}/{pageSize}")]
        public PackagesPaginatedQueryResult Get([FromUri]PackagesPaginatedQuery query)
        {
            return _packagesPaginatedQueryHandler.Handle(query);
        }

        public void Post(SavePackageCommand command)
        {
            _createPackageCommandHandler.Handle(command);
        }

        public void Put(Guid id, UpdatePackageCommand command)
        {
            command.Id = id;
            _updatePackageCommandHandler.Handle(command);
        }

        public void Delete(Guid id)
        {
            _inactivatePackageCommand.Handle(id);
        }
        
        [Route("api/package/{id}/translation")]
        public void Post(AddPackageTranslationCommand command)
        {
            _addPackageTranslationCommandHandler.Handle(command);
        }
    }
}