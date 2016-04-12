using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
                                            .Where(r => r.ProjectId == currentProjectId &&
                                                        r.IsLastVersion &&
                                                        r.SpecificationItem.Active);

            var maxPages = (int)Math.Ceiling(requirementsQuery.Count() / (double)query.PageSize);

            var paginatedQuery = requirementsQuery.OrderBy(r => r.Type)
                                                .ThenBy(r => r.Identifier)
                                                .Include(r => r.RequirementContents)
                                                .Skip(skip)
                                                .Take(query.PageSize);

            var requirements = paginatedQuery.Select(RequirementViewModel.FromModel)
                                             .ToList();

            var itemsIds = requirements.Select(r => r.Id)
                                       .ToList();
            var issues = _context.Issues
                                 .Include(i => i.Contents)
                                 .Include(i => i.IssueComments.Select(ic => ic.Contents))
                                 .Where(i => !i.Concluded && itemsIds.Contains(i.SpecificationItemId))
                                 .Select(IssueViewModel.FromModel)
                                 .ToList();

            foreach(var r in requirements)
                r.Issues = issues.Where(i => i.SpecificationItemId == r.Id);

            var result = new RequirementsPaginatedQueryResult(requirements, maxPages);
            return result;
        }
    }
}
