using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    class UseCaseDiagramVersionsQueryHandler : IQueryHandler<UseCaseDiagramVersionsQuery, IEnumerable<VersionItem>>
    {
        private readonly GSDRequirementsContext _context;

        public UseCaseDiagramVersionsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<VersionItem> Handle(UseCaseDiagramVersionsQuery query)
        {
            var items = _context.UseCaseDiagrams
                                .Where(ucd => ucd.Id == query.Id)
                                .ToList()
                                .Select(ucd => new VersionItem
                                {
                                    Id = ucd.Id,
                                    Version = ucd.Version,
                                    Label = $"{UseCaseDiagram.PREFIX}{ucd.Identifier}"
                                })
                                .OrderBy(v => v.Version);
            return items;
        }
    }
}
