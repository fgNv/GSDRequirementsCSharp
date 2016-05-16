using GSDRequirementsCSharp.Domain;
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
    class ClassDiagramVersionsQueryHandler : IQueryHandler<ClassDiagramVersionsQuery, IEnumerable<VersionItem>>
    {
        private readonly GSDRequirementsContext _context;

        public ClassDiagramVersionsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<VersionItem> Handle(ClassDiagramVersionsQuery query)
        {
            var items = _context.ClassDiagrams
                                .Where(ucd => ucd.Id == query.Id)
                                .ToList()
                                .Select(cd => new VersionItem
                                {
                                    Id = cd.Id,
                                    Version = cd.Version,
                                    Label = $"{ClassDiagram.PREFIX}{cd.Identifier}"
                                })
                                .OrderBy(v => v.Version);
            return items;
        }
    }
}
