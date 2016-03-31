using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    public class CreatePackageCommandHandler : ICommandHandler<SavePackageCommand>
    {
        private readonly IRepository<Package, PackageKey> _packageRepository;
        private readonly ICurrentLocaleName _currentLocaleName;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        
        public CreatePackageCommandHandler(IRepository<Package, PackageKey> packageRepository,
                                           ICurrentLocaleName currentLocaleName,
                                           ICurrentUserRetriever<User> currentUserRetriever,
                                           IRepository<Project, Guid> projectRepository,
                                           ICurrentProjectContextId currentProjectContextId)
        {
            _packageRepository = packageRepository;
            _currentLocaleName = currentLocaleName;
            _currentUserRetriever = currentUserRetriever;
            _projectRepository = projectRepository;
            _currentProjectContextId = currentProjectContextId;
        }

        public void Handle(SavePackageCommand command)
        {
            var package = new Package();
            package.Locale = _currentLocaleName.Get();
            package.Id = Guid.NewGuid();
            package.Description = command.Description;

            var currentUser = _currentUserRetriever.Get();
            package.CreatorId = currentUser.Id;

            var currentProjectId = _currentProjectContextId.Get();
            var project = _projectRepository.Get(currentProjectId);
            package.Project = project;

            _packageRepository.Add(package);
        }
    }
}
