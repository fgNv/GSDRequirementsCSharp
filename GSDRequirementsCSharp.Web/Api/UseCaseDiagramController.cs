using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.Detailed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class UseCaseDiagramController : ApiController
    {
        private readonly IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult> _useCaseDiagramsPaginatedQueryHandler;
        private readonly ICommandHandler<CreateUseCaseDiagramCommand> _createUseCaseDiagramCommandHandler;
        private readonly IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagramDetailedViewModel> _useCaseDiagramDetailQueryHandler;
        private readonly ICommandHandler<CreateUseCaseDiagramNewVersionCommand> _createUseCaseDiagramNewVersionCommandHandler;
        private readonly ICommandHandler<RemoveUseCaseDiagramCommand> _removeUseCaseDiagramCommandHandler;
        private readonly IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> _useCasesByDiagramQueryHandler;
        private readonly IQueryHandler<UseCaseDiagramVersionsQuery, IEnumerable<VersionItem>> _useCaseDiagramVersionsQuery;

        public UseCaseDiagramController(
            IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult> useCaseDiagramsPaginatedQueryHandler,
            ICommandHandler<CreateUseCaseDiagramCommand> createUseCaseDiagramCommandHandler,
            IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagramDetailedViewModel> useCaseDiagramDetailQueryHandler,
            ICommandHandler<CreateUseCaseDiagramNewVersionCommand> createUseCaseDiagramNewVersionCommandHandler,
            ICommandHandler<RemoveUseCaseDiagramCommand> removeUseCaseDiagramCommandHandler,
            IQueryHandler<UseCasesByDiagramQuery, IEnumerable<UseCase>> useCasesByDiagramQueryHandler,
            IQueryHandler<UseCaseDiagramVersionsQuery, IEnumerable<VersionItem>> useCaseDiagramVersionsQuery)
        {
            _useCaseDiagramsPaginatedQueryHandler = useCaseDiagramsPaginatedQueryHandler;
            _createUseCaseDiagramCommandHandler = createUseCaseDiagramCommandHandler;
            _useCaseDiagramDetailQueryHandler = useCaseDiagramDetailQueryHandler;
            _createUseCaseDiagramNewVersionCommandHandler = createUseCaseDiagramNewVersionCommandHandler;
            _removeUseCaseDiagramCommandHandler = removeUseCaseDiagramCommandHandler;
            _useCasesByDiagramQueryHandler = useCasesByDiagramQueryHandler;
            _useCaseDiagramVersionsQuery = useCaseDiagramVersionsQuery;
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