using GSDRequirementsCSharp.Domain.Commands.ClassDiagrams;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
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
        private readonly IQueryHandler<Guid, ClassDiagramDetailedViewModel> _classDiagramDetailQueryHandler;
        private readonly ICommandHandler<CreateClassDiagramNewVersionCommand> _createClassDiagramNewVersionCommandHandler;

        public ClassDiagramController(IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult> classDiagramsPaginatedQuery,
                                      ICommandHandler<CreateClassDiagramCommand> createClassDiagramCommandHandler,
                                      IQueryHandler<Guid, ClassDiagramDetailedViewModel> classDiagramDetailQueryHandler,
                                      ICommandHandler<CreateClassDiagramNewVersionCommand> createClassDiagramNewVersionCommandHandler)
        {
            _classDiagramsPaginatedQuery = classDiagramsPaginatedQuery;
            _createClassDiagramCommandHandler = createClassDiagramCommandHandler;
            _classDiagramDetailQueryHandler = classDiagramDetailQueryHandler;
            _createClassDiagramNewVersionCommandHandler = createClassDiagramNewVersionCommandHandler;
        }

        public ClassDiagramDetailedViewModel Get(Guid id)
        {
            return _classDiagramDetailQueryHandler.Handle(id);
        }

        [HttpGet]
        [Route("api/classDiagram/{page}/{pageSize}")]
        public ClassDiagramsPaginatedQueryResult Get([FromUri]ClassDiagramsPaginatedQuery query)
        {
            var result = _classDiagramsPaginatedQuery.Handle(query);
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