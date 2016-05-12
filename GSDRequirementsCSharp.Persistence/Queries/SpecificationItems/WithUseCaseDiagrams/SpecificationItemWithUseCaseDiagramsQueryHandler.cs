using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems
{
    class SpecificationItemWithUseCaseDiagramsQueryHandler : IQueryHandler<SpecificationItemWithUseCaseDiagramsQuery, SpecificationItemWithUseCaseDiagramsQueryResult>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemWithUseCaseDiagramsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public SpecificationItemWithUseCaseDiagramsQueryResult Handle(SpecificationItemWithUseCaseDiagramsQuery query)
        {

            return _context.SpecificationItems
                           .Where(si => si.Id == query.Id)
                           .Join(_context.UseCaseDiagrams,
                                 si => si.Id,
                                 cd => cd.Id,
                                 (si, ucd) => new
                                 {
                                     UseCaseDiagram = ucd,
                                     SpecificationItem = si
                                 })
                           .GroupBy(r => r.SpecificationItem.Id)
                           .Select(r => new SpecificationItemWithUseCaseDiagramsQueryResult
                           {
                               SpecificationItem = r.FirstOrDefault().SpecificationItem,
                               UseCaseDiagrams = r.Select(i => i.UseCaseDiagram)
                           })
                           .FirstOrDefault();
        }
    }
}
