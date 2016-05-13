﻿using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using System.Data.Entity.SqlServer;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Detailed
{
    internal class UseCaseDiagramDetailQueryHandler : IQueryHandler<Guid, UseCaseDiagramDetailedViewModel>
    {
        private readonly GSDRequirementsContext _context;

        public UseCaseDiagramDetailQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public UseCaseDiagramDetailedViewModel Handle(Guid id)
        {
            var useCaseDiagram = _context.UseCaseDiagrams
                                       .Include(cd => cd.Contents)
                                       .Include(cd => cd.EntitiesRelations.Select(er => er.Contents))
                                       .Include(cd => cd.UseCasesRelations)
                                       .Include(cd => cd.Entities)
                                       .SingleOrDefault(c => c.Id == id &&
                                                             c.Version == _context.UseCaseDiagrams
                                                                                .Where(c1 => c1.Id == id)
                                                                                .Max(c1 => c1.Version));

            if (useCaseDiagram == null)
                return null;

            var entitiesIds = useCaseDiagram.Entities.Select(e => e.Id).ToList();

            var actors = _context.Actors
                                 .Include(u => u.Contents)
                                 .Where(u => u.UseCaseDiagram.Id == id &&
                                             entitiesIds.Contains(u.Id))
                                 .Select(ActorViewModel.FromModel)
                                 .ToList();

            var useCases = _context.UseCases
                                   .Include(u => u.Contents)
                                   .Include(u => u.PreConditions.Select(pc => pc.Contents))
                                   .Include(u => u.PostConditions.Select(pc => pc.Contents))
                                   .Where(u => u.UseCaseDiagram.Id == id &&
                                               entitiesIds.Contains(u.Id))
                                   .Select(UseCaseViewModel.FromModel)
                                   .ToList();

            return UseCaseDiagramDetailedViewModel.FromModel(useCaseDiagram, useCases, actors);
        }
    }
}
