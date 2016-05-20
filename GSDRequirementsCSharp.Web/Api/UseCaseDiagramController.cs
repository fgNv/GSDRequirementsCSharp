using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Converter;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Validation;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class UseCaseDiagramController : ApiController
    {
        private readonly IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult> _useCaseDiagramsPaginatedQueryHandler;
        private readonly ICommandHandler<CreateUseCaseDiagramCommand> _createUseCaseDiagramCommandHandler;
        private readonly IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagramDetailedViewModel> _useCaseDiagramDetailQueryHandler;
        private readonly IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagram> _useCaseDiagramEntityQueryHandler;
        private readonly ICommandHandler<CreateUseCaseDiagramNewVersionCommand> _createUseCaseDiagramNewVersionCommandHandler;
        private readonly ICommandHandler<RemoveUseCaseDiagramCommand> _removeUseCaseDiagramCommandHandler;
        private readonly IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> _useCasesByDiagramQueryHandler;
        private readonly IQueryHandler<UseCaseDiagramVersionsQuery, IEnumerable<VersionItem>> _useCaseDiagramVersionsQuery;
        private readonly IConverter<UseCaseDiagram, CreateUseCaseDiagramNewVersionCommand> _useCaseDiagramToNewVersionCommand;
        private readonly IValidator _validator; 

        public UseCaseDiagramController(
            IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult> useCaseDiagramsPaginatedQueryHandler,
            ICommandHandler<CreateUseCaseDiagramCommand> createUseCaseDiagramCommandHandler,
            IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagramDetailedViewModel> useCaseDiagramDetailQueryHandler,
            ICommandHandler<CreateUseCaseDiagramNewVersionCommand> createUseCaseDiagramNewVersionCommandHandler,
            ICommandHandler<RemoveUseCaseDiagramCommand> removeUseCaseDiagramCommandHandler,
            IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> useCasesByDiagramQueryHandler,
            IConverter<UseCaseDiagram, CreateUseCaseDiagramNewVersionCommand> useCaseDiagramToNewVersionCommand,
            IQueryHandler<UseCaseDiagramVersionsQuery, IEnumerable<VersionItem>> useCaseDiagramVersionsQuery,
            IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagram> useCaseDiagramEntityQueryHandler,
            IValidator validator)
        {
            _useCaseDiagramsPaginatedQueryHandler = useCaseDiagramsPaginatedQueryHandler;
            _createUseCaseDiagramCommandHandler = createUseCaseDiagramCommandHandler;
            _useCaseDiagramDetailQueryHandler = useCaseDiagramDetailQueryHandler;
            _createUseCaseDiagramNewVersionCommandHandler = createUseCaseDiagramNewVersionCommandHandler;
            _removeUseCaseDiagramCommandHandler = removeUseCaseDiagramCommandHandler;
            _useCasesByDiagramQueryHandler = useCasesByDiagramQueryHandler;
            _useCaseDiagramVersionsQuery = useCaseDiagramVersionsQuery;
            _useCaseDiagramEntityQueryHandler = useCaseDiagramEntityQueryHandler;
            _useCaseDiagramToNewVersionCommand = useCaseDiagramToNewVersionCommand;
            _validator = validator;
        }

        public UseCaseDiagramDetailedViewModel Get([FromUri]UseCaseDiagramDetailQuery query)
        {
            return _useCaseDiagramDetailQueryHandler.Handle(query);
        }

        [HttpGet]
        [Route("api/useCaseDiagram/{id}/versions")]
        public IEnumerable<VersionItem> Versions([FromUri]UseCaseDiagramVersionsQuery query)
        {
            var result = _useCaseDiagramVersionsQuery.Handle(query);
            return result;
        }

        [HttpPost]
        [Route("api/useCaseDiagram/{id}/versions")]
        public void Versions(RestoreVersionCommand command)
        {
            _validator.Validate(command);
            var useCaseDiagram = _useCaseDiagramEntityQueryHandler.Handle(command);
            var newVersionCommand = _useCaseDiagramToNewVersionCommand.Convert(useCaseDiagram);
            _createUseCaseDiagramNewVersionCommandHandler.Handle(newVersionCommand);
        }

        [HttpGet]
        [Route("api/useCaseDiagram/{page}/{pageSize}")]
        public UseCaseDiagramsPaginatedQueryResult Get([FromUri]UseCaseDiagramsPaginatedQuery query)
        {
            var result = _useCaseDiagramsPaginatedQueryHandler.Handle(query);
            return result;
        }
        
        [HttpGet]
        [Route("api/useCaseDiagram/{diagramId}/useCases")]
        public IEnumerable<UseCaseArtifactViewModel> Get([FromUri]UseCasesByDiagramQuery query)
        {
            var result = _useCasesByDiagramQueryHandler.Handle(query);
            return result.Select(i => UseCaseArtifactViewModel.FromModel(i, i.SpecificationItem))
                         .ToList();
        }

        public void Post(CreateUseCaseDiagramCommand command)
        {
            _createUseCaseDiagramCommandHandler.Handle(command);
        }

        public void Put(CreateUseCaseDiagramNewVersionCommand command)
        {
            _createUseCaseDiagramNewVersionCommandHandler.Handle(command);
        }

        public void Delete([FromUri]RemoveUseCaseDiagramCommand command)
        {
            _removeUseCaseDiagramCommandHandler.Handle(command);
        }
    }
}