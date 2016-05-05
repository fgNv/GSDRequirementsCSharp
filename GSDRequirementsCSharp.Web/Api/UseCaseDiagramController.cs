using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams;
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
        private readonly IQueryHandler<Guid, UseCaseDiagramDetailedViewModel> _useCaseDiagramDetailQueryHandler;
        private readonly ICommandHandler<CreateUseCaseDiagramNewVersionCommand> _createUseCaseDiagramNewVersionCommandHandler;

        public UseCaseDiagramController(
            IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult> useCaseDiagramsPaginatedQueryHandler,
            ICommandHandler<CreateUseCaseDiagramCommand> createUseCaseDiagramCommandHandler,
            IQueryHandler<Guid, UseCaseDiagramDetailedViewModel> useCaseDiagramDetailQueryHandler,
            ICommandHandler<CreateUseCaseDiagramNewVersionCommand> createUseCaseDiagramNewVersionCommandHandler)
        {
            _useCaseDiagramsPaginatedQueryHandler = useCaseDiagramsPaginatedQueryHandler;
            _createUseCaseDiagramCommandHandler = createUseCaseDiagramCommandHandler;
            _useCaseDiagramDetailQueryHandler = useCaseDiagramDetailQueryHandler;
            _createUseCaseDiagramNewVersionCommandHandler = createUseCaseDiagramNewVersionCommandHandler;
        }

        public UseCaseDiagramDetailedViewModel Get(Guid id)
        {
            return _useCaseDiagramDetailQueryHandler.Handle(id);
        }

        [HttpGet]
        [Route("api/useCaseDiagram/{page}/{pageSize}")]
        public UseCaseDiagramsPaginatedQueryResult Get([FromUri]UseCaseDiagramsPaginatedQuery query)
        {
            var result = _useCaseDiagramsPaginatedQueryHandler.Handle(query);
            return result;
        }

        public void Post(CreateUseCaseDiagramCommand command)
        {
            _createUseCaseDiagramCommandHandler.Handle(command);
        }

        public void Put(CreateUseCaseDiagramNewVersionCommand command)
        {
            _createUseCaseDiagramNewVersionCommandHandler.Handle(command);
        }
    }
}