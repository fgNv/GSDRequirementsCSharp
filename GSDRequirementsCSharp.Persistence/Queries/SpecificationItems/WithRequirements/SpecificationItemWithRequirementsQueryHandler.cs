using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Linq;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.WithRequirements
{
    internal class SpecificationItemWithRequirementsQueryHandler : IQueryHandler<SpecificationItemWithRequirementsQuery, SpecificationItemWithRequirementsQueryResult>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemWithRequirementsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }    

        public SpecificationItemWithRequirementsQueryResult Handle(SpecificationItemWithRequirementsQuery query)
        {
            return _context.SpecificationItems
                           .Where(si => si.Id == query.Id)
                           .Join(_context.Requirements,
                                 si => si.Id,
                                 cd => cd.Id,
                                 (si, r) => new
                                 {
                                     Requirement = r,
                                     SpecificationItem = si
                                 })
                           .GroupBy(r => r.SpecificationItem.Id)
                           .Select(r => new SpecificationItemWithRequirementsQueryResult
                           {
                               SpecificationItem = r.FirstOrDefault().SpecificationItem,
                               Requirements = r.Select(i => i.Requirement)
                           })
                           .FirstOrDefault();
        }
    }
}
