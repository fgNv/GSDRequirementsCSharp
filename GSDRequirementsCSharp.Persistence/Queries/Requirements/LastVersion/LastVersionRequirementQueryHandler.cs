using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Requirements.LastVersion
{
    internal class LastVersionRequirementQueryHandler : IQueryHandler<LastVersionRequirementQuery, Requirement>
    {
        private readonly GSDRequirementsContext _context;

        public LastVersionRequirementQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Requirement Handle(LastVersionRequirementQuery query)
        {
            return _context.Requirements
                           .FirstOrDefault(r => r.IsLastVersion && r.Id == query.Id);
        }
    }
}
