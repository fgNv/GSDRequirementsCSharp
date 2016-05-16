using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Requirements
{
    internal class LastVersionRequirementQueryHandler : IQueryHandler<DetailedRequirementQuery, Requirement>
    {
        private readonly GSDRequirementsContext _context;

        public LastVersionRequirementQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public Requirement Handle(DetailedRequirementQuery query)
        {
            return _context.Requirements
                           .Include(r => r.RequirementContents)
                           .Include(r => r.SpecificationItem.Package.Contents)
                           .FirstOrDefault(r => r.Id == query.Id && 
                                                (r.IsLastVersion && !query.Version.HasValue ||
                                                 r.Version == query.Version.Value));
        }
    }
}
