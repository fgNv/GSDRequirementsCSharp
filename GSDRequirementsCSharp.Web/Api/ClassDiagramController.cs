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

        public ClassDiagramController(IQueryHandler<ClassDiagramsPaginatedQuery, ClassDiagramsPaginatedQueryResult> classDiagramsPaginatedQuery)
        {
            _classDiagramsPaginatedQuery = classDiagramsPaginatedQuery;
        }

        [HttpGet]
        [Route("api/classDiagram/{page}/{pageSize}")]
        public ClassDiagramsPaginatedQueryResult Get([FromUri]ClassDiagramsPaginatedQuery query)
        {
            var result = _classDiagramsPaginatedQuery.Handle(query);
            return result;
        }
    }
}