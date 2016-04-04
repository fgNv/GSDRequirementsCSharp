using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class RequirementsPaginatedQueryHandler : IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public RequirementsPaginatedQueryHandler(GSDRequirementsContext context,
                                             ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public RequirementsPaginatedQueryResult Handle(RequirementsPaginatedQuery query)
        {
            var skip = (query.Page - 1) * query.PageSize;
            var currentProjectId = _currentProjectContextId.Get();

            var requirementsQuery = _context.Requirements
                                            .Include(r => r.SpecificationItem.Package)
                                            .Where(p => p.SpecificationItem.Package.Project.Id == currentProjectId && 
                                                        p.SpecificationItem.Active);

            var maxPages = (int)Math.Ceiling(requirementsQuery.Count() / (double)query.PageSize);

            var requirements = requirementsQuery.OrderBy(r => r.Identifier)
                                        .Include(r => r.RequirementContents)
                                        .Skip(skip)
                                        .Take(query.PageSize)
                                        .ToList();
                        
            var result = new RequirementsPaginatedQueryResult(requirements, maxPages);
            return result;
        }
    }
}
