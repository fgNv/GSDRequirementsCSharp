using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
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
        private readonly IQueryHandler<SequenceDiagramNextIdQuery, int> _sequenceDiagramNextIdQueryHandler;

        public CreateSequenceDiagramCommandHandler(
            IRepository<SequenceDiagram, VersionKey> sequenceDiagramRepository,
            IRepository<SequenceDiagramContent, LocaleKey> sequenceDiagramContentRepository,
            ICurrentProjectContextId currentProjectContextId,
            ICurrentUserRetriever<User> currentUserRetriever,
            IRepository<SpecificationItem, Guid> specificationItemtRepository,
            IQueryHandler<SequenceDiagramNextIdQuery, int> sequenceDiagramNextIdQueryHandler)
        {
            _sequenceDiagramRepository = sequenceDiagramRepository;
            _sequenceDiagramContentRepository = sequenceDiagramContentRepository;
            _currentProjectContextId = currentProjectContextId;
            _currentUserRetriever = currentUserRetriever;
            _specificationItemtRepository = specificationItemtRepository;
            _sequenceDiagramNextIdQueryHandler = sequenceDiagramNextIdQueryHandler;
        }

        private void PersistContent(SaveSequenceDiagramCommand command, 
                                    SequenceDiagram sequenceDiagram)
        {
            var currentUser = _currentUserRetriever.Get();

            foreach (var content in command.Contents)
            {
                var contentModel = new SequenceDiagramContent();
                contentModel.Id = Guid.NewGuid();
                contentModel.Description = content.Description;
                contentModel.Locale = content.Locale;
                contentModel.SequenceDiagram = sequenceDiagram;
                contentModel.Version = 1;
                contentModel.CreatorId = currentUser.Id;

                _sequenceDiagramContentRepository.Add(contentModel);
            }
        }

        private void PersistSpecificationItem(SaveSequenceDiagramCommand command, 
                                              Guid? currentProjectId,
                                              SequenceDiagram sequenceDiagram)
        {
            var nextId = _sequenceDiagramNextIdQueryHandler.Handle(currentProjectId.Value);

            var specificationItem = new SpecificationItem();
            specificationItem.Active = true;
            specificationItem.Id = command.SequenceDiagramId.Value;
            specificationItem.PackageId = command.PackageId.Value;
            specificationItem.Label = $"SD{nextId}";

            _specificationItemtRepository.Add(specificationItem);
            sequenceDiagram.SpecificationItem = specificationItem;
        }

        public void Handle(SaveSequenceDiagramCommand command)
        {
            var sequenceDiagram = new SequenceDiagram();
            sequenceDiagram.Id = command.SequenceDiagramId.Value;

            var currentProjectId = _currentProjectContextId.Get();
            if (currentProjectId == null)
                throw new Exception(Sentences.projectDefinedInTheContextRequired);
            sequenceDiagram.ProjectId = currentProjectId.Value;

            PersistSpecificationItem(command, currentProjectId, sequenceDiagram);
            _sequenceDiagramRepository.Add(sequenceDiagram);            
            PersistContent(command, sequenceDiagram);
        }
    }
}
