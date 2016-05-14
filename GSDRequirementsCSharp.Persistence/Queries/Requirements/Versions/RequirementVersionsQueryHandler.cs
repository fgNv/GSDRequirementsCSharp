using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GSDRequirementsCSharp.Domain.ViewModels.RequirementViewModel;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    class RequirementVersionsQueryHandler : IQueryHandler<RequirementVersionsQuery, IEnumerable<VersionItem>>
    {
        private readonly GSDRequirementsContext _context;

        public RequirementVersionsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<VersionItem> Handle(RequirementVersionsQuery query)
        {
            var items = _context.Requirements
                                .Where(ucd => ucd.Id == query.Id)
                                .ToList()
                                .Select(r => new VersionItem
                                {
                                    Id = r.Id,
                                    Version = r.Version,
                                    Label = $"{GetPrefixFromType(r.Type)}{r.Identifier}"
                                })
                                .OrderBy(v => v.Version);
            return items;
        }
    }
}
