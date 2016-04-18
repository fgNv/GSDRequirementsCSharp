using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.ByProject
{
    class ItemsByProjectQueryHandler : IQueryHandler<ItemsByProjectQuery, IEnumerable<SpecificationItemViewModel>>
    {
        private readonly GSDRequirementsContext _context;

        public ItemsByProjectQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<SpecificationItemViewModel> Handle(ItemsByProjectQuery query)
        {
            var items = _context.SpecificationItems
                                .Include(s => s.Package.Contents)
                                .Where(s => s.Package.ProjectId == query.ProjectId &&
                                            s.Active)
                                .Select(SpecificationItemViewModel.FromModel)
                                .ToList();

            return items;
        }
    }
}
