using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.Auditings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class AuditingController : ApiController
    {
        private readonly IQueryHandler<AuditingsByPeriodQuery, IEnumerable<AuditingViewModel>> _auditingsByPeriodQueryHandler;
        private readonly IQueryHandler<AuditingsByProjectQuery, IEnumerable<AuditingViewModel>> _auditingsByProjectQueryHandler;

        public AuditingController(IQueryHandler<AuditingsByPeriodQuery, IEnumerable<AuditingViewModel>> auditingsByPeriodQueryHandler,
                                  IQueryHandler<AuditingsByProjectQuery, IEnumerable<AuditingViewModel>> auditingsByProjectQueryHandler)
        {
            _auditingsByPeriodQueryHandler = auditingsByPeriodQueryHandler;
            _auditingsByProjectQueryHandler = auditingsByProjectQueryHandler;
        }

        public IEnumerable<AuditingViewModel> Get([FromUri]AuditingsByPeriodQuery query)
        {
            var auditings = _auditingsByPeriodQueryHandler.Handle(query);
            return auditings;
        }

        [HttpGet]
        [Route("api/project/{projectId}/auditing")]
        public IEnumerable<AuditingViewModel> Get([FromUri] AuditingsByProjectQuery query)
        {
            var auditings = _auditingsByProjectQueryHandler.Handle(query);
            return auditings;
        }
    }
}