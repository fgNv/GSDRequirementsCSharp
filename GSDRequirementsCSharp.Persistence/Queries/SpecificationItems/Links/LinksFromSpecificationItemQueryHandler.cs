using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Infrastructure.Internationalization;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.Links
{
    class LinksFromSpecificationItemQueryHandler : IQueryHandler<LinksFromSpecificationItemQuery, IEnumerable<ItemLinkViewModel>>
    {
        private readonly GSDRequirementsContext _context;

        public LinksFromSpecificationItemQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<ItemLinkViewModel> Handle(LinksFromSpecificationItemQuery query)
        {
            var speficiationItem = _context.SpecificationItems
                                           .Include(si => si.LinkedItems)
                                           .Include(si => si.Package.Contents)
                                           .FirstOrDefault(si => si.Id == query.Id);

            if (speficiationItem == null)
                throw new Exception(Sentences.specificationItemNotFound);

            var links = speficiationItem.LinkedItems
                                        .Select(t => ItemLinkViewModel.FromModel(speficiationItem, t))
                                        .OrderBy(s => s.Target.Label)
                                        .ToList();

            return links;
        }
    }
}
