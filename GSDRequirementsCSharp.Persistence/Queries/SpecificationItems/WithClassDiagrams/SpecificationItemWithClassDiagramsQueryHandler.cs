﻿using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.SpecificationItems;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.WithClassDiagrams
{
    class SpecificationItemWithClassDiagramsQueryHandler : IQueryHandler<SpecificationItemWithClassDiagramsQuery, SpecificationItemWithClassDiagramsQueryResult>
    {
        private readonly GSDRequirementsContext _context;

        public SpecificationItemWithClassDiagramsQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public SpecificationItemWithClassDiagramsQueryResult Handle(SpecificationItemWithClassDiagramsQuery query)
        {
            return _context.SpecificationItems
                           .Where(si => si.Id == query.Id)
                           .Join(_context.ClassDiagrams,
                                 si => si.Id,
                                 cd => cd.Id,
                                 (si, cd) => new
                                 {
                                     ClassDiagram = cd,
                                     SpecificationItem = si
                                 })
                           .GroupBy(r => r.SpecificationItem.Id)
                           .Select(r => new SpecificationItemWithClassDiagramsQueryResult
                           {
                               SpecificationItem = r.FirstOrDefault().SpecificationItem,
                               ClassDiagrams = r.Select(i => i.ClassDiagram)
                           })
                           .FirstOrDefault();
        }
    }
}
