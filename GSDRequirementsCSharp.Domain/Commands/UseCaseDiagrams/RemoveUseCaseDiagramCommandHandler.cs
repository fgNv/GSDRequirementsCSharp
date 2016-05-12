using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams
{
    public class RemoveUseCaseDiagramCommandHandler : ICommandHandler<RemoveUseCaseDiagramCommand>
    {
        private readonly IRepository<SpecificationItem, Guid> _specificationItemRepository;
        private readonly IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> _useCasesByDiagramQueryHandler;

        public RemoveUseCaseDiagramCommandHandler(IRepository<SpecificationItem, Guid> specificationItemRepository,
                                                  IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> useCasesByDiagramQueryHandler)
        {
            _specificationItemRepository = specificationItemRepository;
            _useCasesByDiagramQueryHandler = useCasesByDiagramQueryHandler;
        }

        public void Handle(RemoveUseCaseDiagramCommand command)
        {
            var specificationItem = _specificationItemRepository.Get(command.Id);
            if (specificationItem == null)
                throw new Exception(Sentences.useCaseDiagramNotFound);

            var useCases = _useCasesByDiagramQueryHandler.Handle(command.Id);
            foreach (var useCase in useCases)
                useCase.SpecificationItem.Active = false;

            specificationItem.Active = false;
        }
    }
}
