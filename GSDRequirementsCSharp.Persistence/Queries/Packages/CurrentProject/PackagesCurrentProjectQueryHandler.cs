using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.CurrentProject
{
    public class PackagesCurrentProjectQuery
    {

    }

    public class PackagesCurrentProjectQueryHandler : IQueryHandler<PackagesCurrentProjectQuery, IEnumerable<Package>>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public PackagesCurrentProjectQueryHandler(GSDRequirementsContext context,
                                                  ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public IEnumerable<Package> Handle(PackagesCurrentProjectQuery query)
        {
            var projectId = _currentProjectContextId.Get();
            var packages = _context.Packages
                                   .Include(p => p.Contents)
                                   .Where(p => p.Project.Id == projectId)
                                   .ToList();

            return packages;
        }
    }
}
