using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries.Packages;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
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
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly IQueryHandler<PackageNextIdQuery, int> _packageNextIdQueryHandler;

        public CreatePackageCommandHandler(IRepository<Package, Guid> packageRepository,
                                           IRepository<PackageContent, LocaleKey> packageContentRepository,
                                           ICurrentUserRetriever<User> currentUserRetriever,
                                           IRepository<Project, Guid> projectRepository,
                                           ICurrentProjectContextId currentProjectContextId,
                                           IQueryHandler<PackageNextIdQuery, int> packageNextIdQueryHandler)
        {
            _packageRepository = packageRepository;
            _packageContentRepository = packageContentRepository;
            _currentUserRetriever = currentUserRetriever;
            _projectRepository = projectRepository;
            _currentProjectContextId = currentProjectContextId;
            _packageNextIdQueryHandler = packageNextIdQueryHandler;
        }

        public void Handle(SavePackageCommand command)
        {
            var package = new Package();
            package.Id = Guid.NewGuid();

            foreach(var item in command.Items)
            {
                var content = new PackageContent();
                content.Id = package.Id;
                content.Locale = item.Locale;
                content.Description = item.Description;
                content.Package = package;
                content.IsUpdated = true;
                _packageContentRepository.Add(content);
            }

            var currentUser = _currentUserRetriever.Get();
            package.CreatorId = currentUser.Id;


            var currentProjectId = _currentProjectContextId.Get();
            if (currentProjectId == null)
                throw new Exception(Sentences.noProjectInContext);

            var project = _projectRepository.Get(currentProjectId.Value);
            package.Project = project;
            package.Active = true;

            var nextId = _packageNextIdQueryHandler.Handle(project.Id);
            package.Identifier = nextId;

            _packageRepository.Add(package);
        }
    }
}
