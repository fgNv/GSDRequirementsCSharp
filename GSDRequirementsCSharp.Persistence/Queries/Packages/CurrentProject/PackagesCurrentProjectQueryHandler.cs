using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.ViewModels;

namespace GSDRequirementsCSharp.Persistence.Queries.Packages.CurrentProject
{
    public class PackagesCurrentProjectQuery
    {

    }

    internal class PackagesCurrentProjectQueryHandler : IQueryHandler<PackagesCurrentProjectQuery, IEnumerable<PackageViewModel>>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public PackagesCurrentProjectQueryHandler(GSDRequirementsContext context,
                                                  ICurrentProjectContextId currentProjectContextId)
        {
            _context = context;
            _currentProjectContextId = currentProjectContextId;
        }

        public IEnumerable<PackageViewModel> Handle(PackagesCurrentProjectQuery query)
        {
            var projectId = _currentProjectContextId.Get();
            var packages = _context.Packages
                                   .Include(p => p.Contents)
                                   .Where(p => p.Project.Id == projectId)
                                   .Select(PackageViewModel.FromModel)
                                   .ToList();
            return packages;
        }
    }
}
