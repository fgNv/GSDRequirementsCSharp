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

        public UseCaseDiagramController(IQueryHandler<UseCaseDiagramsPaginatedQuery, UseCaseDiagramsPaginatedQueryResult> useCaseDiagramsPaginatedQueryHandler)
        {
            _useCaseDiagramsPaginatedQueryHandler = useCaseDiagramsPaginatedQueryHandler;
        }

        [HttpGet]
        [Route("api/useCaseDiagram/{page}/{pageSize}")]
        public UseCaseDiagramsPaginatedQueryResult Get([FromUri]UseCaseDiagramsPaginatedQuery query)
        {
            var result = _useCaseDiagramsPaginatedQueryHandler.Handle(query);
            return result;
        }
    }
}