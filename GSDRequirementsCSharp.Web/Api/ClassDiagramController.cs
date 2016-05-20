using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Converter;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Validation;
using GSDRequirementsCSharp.Persistence.Queries;
using GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Paginated;
using System.Collections.Generic;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class ClassDiagramController : ApiController
    {
        private readonly IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult> _classDiagramsPaginatedQuery;
        private readonly ICommandHandler<CreateClassDiagramCommand> _createClassDiagramCommandHandler;
        private readonly IQueryHandler<ClassDiagramDetailQuery, ClassDiagramDetailedViewModel> _classDiagramDetailQueryHandler;
        private readonly IQueryHandler<ClassDiagramDetailQuery, ClassDiagram> _classDiagramEntityQueryHandler;
        private readonly ICommandHandler<CreateClassDiagramNewVersionCommand> _createClassDiagramNewVersionCommandHandler;
        private readonly IQueryHandler<ClassDiagramVersionsQuery, IEnumerable<VersionItem>> _classDiagramVersionsQuery;
        private readonly IConverter<ClassDiagram, CreateClassDiagramNewVersionCommand> _classDiagramToNewVersionCommand;
        private readonly IValidator _validator;
        private readonly ICommandHandler<RemoveClassDiagramCommand> _removeClassDiagramCommandHandler;

        public ClassDiagramController(IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult> classDiagramsPaginatedQuery,
                                      ICommandHandler<CreateClassDiagramCommand> createClassDiagramCommandHandler,
                                      IQueryHandler<ClassDiagramDetailQuery, ClassDiagramDetailedViewModel> classDiagramDetailQueryHandler,
                                      ICommandHandler<CreateClassDiagramNewVersionCommand> createClassDiagramNewVersionCommandHandler,
                                      IQueryHandler<ClassDiagramVersionsQuery, IEnumerable<VersionItem>> classDiagramVersionsQuery,
                                      IQueryHandler<ClassDiagramDetailQuery, ClassDiagram> classDiagramEntityQueryHandler,
                                      IConverter<ClassDiagram, CreateClassDiagramNewVersionCommand> classDiagramToNewVersionCommand,
                                      IValidator validator,
                                      ICommandHandler<RemoveClassDiagramCommand> removeClassDiagramCommandHandler)
        {
            _classDiagramsPaginatedQuery = classDiagramsPaginatedQuery;
            _createClassDiagramCommandHandler = createClassDiagramCommandHandler;
            _classDiagramDetailQueryHandler = classDiagramDetailQueryHandler;
            _createClassDiagramNewVersionCommandHandler = createClassDiagramNewVersionCommandHandler;
            _classDiagramVersionsQuery = classDiagramVersionsQuery;
            _classDiagramToNewVersionCommand = classDiagramToNewVersionCommand;
            _validator = validator;
            _classDiagramEntityQueryHandler = classDiagramEntityQueryHandler;
            _removeClassDiagramCommandHandler = removeClassDiagramCommandHandler;
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

        [HttpPost]
        [Route("api/classDiagram/{id}/versions")]
        public void Versions(RestoreVersionCommand command)
        {
            _validator.Validate(command);
            var useCaseDiagram = _classDiagramEntityQueryHandler.Handle(command);
            var newVersionCommand = _classDiagramToNewVersionCommand.Convert(useCaseDiagram);
            _createClassDiagramNewVersionCommandHandler.Handle(newVersionCommand);
        }

        public void Post(CreateClassDiagramCommand command)
        {
            _createClassDiagramCommandHandler.Handle(command);
        }

        public void Put(CreateClassDiagramNewVersionCommand command)
        {
            _createClassDiagramNewVersionCommandHandler.Handle(command);
        }

        public void Delete([FromUri]RemoveClassDiagramCommand command)
        {
            _removeClassDiagramCommandHandler.Handle(command);
        }
    }
}