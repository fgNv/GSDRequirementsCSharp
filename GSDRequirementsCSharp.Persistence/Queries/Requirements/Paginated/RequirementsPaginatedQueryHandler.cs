using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    internal class RequirementsPaginatedQueryHandler : IQueryHandler<RequirementsPaginatedQuery, RequirementsPaginatedQueryResult>
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
                                            .Include(r => r.SpecificationItem.Issues)
                                            .Where(p => p.SpecificationItem.Package.Project.Id == currentProjectId &&
                                                        p.IsLastVersion &&
                                                        p.SpecificationItem.Active);

            var maxPages = (int)Math.Ceiling(requirementsQuery.Count() / (double)query.PageSize);

            var requirements = requirementsQuery.OrderBy(r => r.Type)
                                                .ThenBy(r => r.Identifier)
                                                .Include(r => r.RequirementContents)
                                                .Skip(skip)
                                                .Take(query.PageSize)
                                                .Select(r => new RequirementViewModel
                                                {
                                                    RequirementContents = r.RequirementContents,
                                                    Difficulty = r.Difficulty,
                                                    Id = r.Id,
                                                    Package = r.SpecificationItem.Package,
                                                    PackageId = r.SpecificationItem.PackageId,
                                                    Identifier = r.Identifier,
                                                    RequirementType = r.Type,
                                                    Type = r.Type,
                                                    Issues = r.SpecificationItem
                                                              .Issues
                                                              .Where(i => !i.Concluded)
                                                })
                                                .ToList();

            var result = new RequirementsPaginatedQueryResult(requirements, maxPages);
            return result;
        }
    }
}
