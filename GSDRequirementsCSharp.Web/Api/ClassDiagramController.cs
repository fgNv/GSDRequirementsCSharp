using GSDRequirementsCSharp.Domain.Commands.ClassDiagrams;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Detailed;
using GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Paginated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class ClassDiagramController : ApiController
    {
        private readonly IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult> _classDiagramsPaginatedQuery;
        private readonly ICommandHandler<CreateClassDiagramCommand> _createClassDiagramCommandHandler;
        private readonly IQueryHandler<ClassDiagramDetailQuery, ClassDiagramDetailedViewModel> _classDiagramDetailQueryHandler;
        private readonly ICommandHandler<CreateClassDiagramNewVersionCommand> _createClassDiagramNewVersionCommandHandler;
        private readonly IQueryHandler<ClassDiagramVersionsQuery, IEnumerable<VersionItem>> _classDiagramVersionsQuery;

        public ClassDiagramController(IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult> classDiagramsPaginatedQuery,
                                      ICommandHandler<CreateClassDiagramCommand> createClassDiagramCommandHandler,
                                      IQueryHandler<ClassDiagramDetailQuery, ClassDiagramDetailedViewModel> classDiagramDetailQueryHandler,
                                      ICommandHandler<CreateClassDiagramNewVersionCommand> createClassDiagramNewVersionCommandHandler,
                                      IQueryHandler<ClassDiagramVersionsQuery, IEnumerable<VersionItem>> classDiagramVersionsQuery)
        {
            _classDiagramsPaginatedQuery = classDiagramsPaginatedQuery;
            _createClassDiagramCommandHandler = createClassDiagramCommandHandler;
            _classDiagramDetailQueryHandler = classDiagramDetailQueryHandler;
            _createClassDiagramNewVersionCommandHandler = createClassDiagramNewVersionCommandHandler;
            _classDiagramVersionsQuery = classDiagramVersionsQuery;
        }

        public ClassDiagramDetailedViewModel Get([FromUri]ClassDiagramDetailQuery query)
        {
            return _classDiagramDetailQueryHandler.Handle(query);
        }

        [HttpGet]
        [Route("api/classDiagram/{page}/{pageSize}")]
        public ClassDiagramsPaginatedQueryResult Get([FromUri]ClassDiagramsPaginatedQuery query)
        {
            var result = _classDiagramsPaginatedQuery.Handle(query);
            return result;
        }

        [HttpGet]
        [Route("api/classDiagram/{id}/versions")]
        public IEnumerable<VersionItem> Versions([FromUri]ClassDiagramVersionsQuery query)
        {
            var result = _classDiagramVersionsQuery.Handle(query);
            return result;
        }

        public void Post(CreateClassDiagramCommand command)
        {
            _createClassDiagramCommandHandler.Handle(command);
        }

        public void Put(CreateClassDiagramNewVersionCommand command)
        {
            _createClassDiagramNewVersionCommandHandler.Handle(command);
        }
    }
}