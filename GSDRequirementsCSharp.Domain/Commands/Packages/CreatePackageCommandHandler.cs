using GSDRequirementsCSharp.Domain.Models;
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
        private readonly IRepository<Package, Guid> _packageRepository;
        private readonly IRepository<PackageContent, LocaleKey> _packageContentRepository;
        private readonly ICurrentLocaleName _currentLocaleName;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        
        public CreatePackageCommandHandler(IRepository<Package, Guid> packageRepository,
                                           IRepository<PackageContent, LocaleKey> packageContentRepository,
                                           ICurrentLocaleName currentLocaleName,
                                           ICurrentUserRetriever<User> currentUserRetriever,
                                           IRepository<Project, Guid> projectRepository,
                                           ICurrentProjectContextId currentProjectContextId)
        {
            _packageRepository = packageRepository;
            _packageContentRepository = packageContentRepository;
            _currentLocaleName = currentLocaleName;
            _currentUserRetriever = currentUserRetriever;
            _projectRepository = projectRepository;
            _currentProjectContextId = currentProjectContextId;
        }

        public void Handle(SavePackageCommand command)
        {
            var package = new Package();
            var content = new PackageContent();
            content.Locale = _currentLocaleName.Get();
            package.Id = Guid.NewGuid();
            content.Id = package.Id;
            content.Description = command.Description;
            content.Package = package;

            var currentUser = _currentUserRetriever.Get();
            package.CreatorId = currentUser.Id;

            var currentProjectId = _currentProjectContextId.Get();
            var project = _projectRepository.Get(currentProjectId);
            package.Project = project;
            package.Active = true;

            _packageRepository.Add(package);
            _packageContentRepository.Add(content);
        }
    }
}
