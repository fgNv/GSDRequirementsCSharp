using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    class CreateSequenceDiagramCommandHandler : ICommandHandler<SaveSequenceDiagramCommand>
    {
        private readonly IRepository<SequenceDiagram, VersionKey> _sequenceDiagramRepository;
        private readonly IRepository<SequenceDiagramContent, LocaleKey> _sequenceDiagramContentRepository;
        private readonly IRepository<SpecificationItem, Guid> _specificationItemtRepository;
        private readonly ICurrentProjectContextId _currentProjectContextId;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public CreateSequenceDiagramCommandHandler(
            IRepository<SequenceDiagram, VersionKey> sequenceDiagramRepository,
            IRepository<SequenceDiagramContent, LocaleKey> sequenceDiagramContentRepository,
            ICurrentProjectContextId currentProjectContextId,
            ICurrentUserRetriever<User> currentUserRetriever,
            IRepository<SpecificationItem, Guid> specificationItemtRepository)
        {
            _sequenceDiagramRepository = sequenceDiagramRepository;
            _sequenceDiagramContentRepository = sequenceDiagramContentRepository;
            _currentProjectContextId = currentProjectContextId;
            _currentUserRetriever = currentUserRetriever;
            _specificationItemtRepository = specificationItemtRepository;
        }

        public void Handle(SaveSequenceDiagramCommand command)
        {
            var sequenceDiagram = new SequenceDiagram();
            sequenceDiagram.Id = command.SequenceDiagramId.Value;
            var currentProjectId = _currentProjectContextId.Get();
            if (currentProjectId == null)
                throw new Exception(Sentences.projectDefinedInTheContextRequired);
            sequenceDiagram.ProjectId = currentProjectId.Value;

            var specificationItem = new SpecificationItem();
            specificationItem.Active = true;
            specificationItem.Id = command.SequenceDiagramId.Value;
            //specificationItem.PackageId = 
            //sequenceDiagram.SpecificationItem
        }
    }
}
