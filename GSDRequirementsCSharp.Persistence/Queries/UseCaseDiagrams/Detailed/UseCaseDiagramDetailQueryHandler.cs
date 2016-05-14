using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using System.Data.Entity.SqlServer;
using GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.Detailed;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Detailed
{
    internal class UseCaseDiagramDetailQueryHandler : IQueryHandler<UseCaseDiagramDetailQuery, UseCaseDiagramDetailedViewModel>
    {
        private readonly GSDRequirementsContext _context;

        public UseCaseDiagramDetailQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public UseCaseDiagramDetailedViewModel Handle(UseCaseDiagramDetailQuery query)
        {
            var useCaseDiagram = _context.UseCaseDiagrams
                                       .Include(cd => cd.Contents)
                                       .Include(cd => cd.EntitiesRelations.Select(er => er.Contents))
                                       .Include(cd => cd.EntitiesRelations.Select(er => er.Source))
                                       .Include(cd => cd.EntitiesRelations.Select(er => er.Target))
                                       .Include(cd => cd.UseCasesRelations)
                                       .Include(cd => cd.Entities)
                                       .SingleOrDefault(c => c.Id == query.Id && (
                                       c.IsLastVersion && !query.Version.HasValue || 
                                       c.Version == query.Version.Value));

            if (useCaseDiagram == null)
                return null;

            var entitiesIds = useCaseDiagram.Entities
                                            .Select(e => e.Id)
                                            .ToList();

            var actors = _context.Actors
                                 .Include(u => u.Contents)
                                 .Where(u => u.UseCaseDiagram.Id == query.Id &&
                                             entitiesIds.Contains(u.Id) && u.Version == useCaseDiagram.Version)
                                 .Select(ActorViewModel.FromModel)
                                 .ToList();

            var useCases = _context.UseCases
                                   .Include(u => u.Contents)
                                   .Include(u => u.PreConditions.Select(pc => pc.Contents))
                                   .Include(u => u.PostConditions.Select(pc => pc.Contents))
                                   .Where(u => u.UseCaseDiagram.Id == query.Id && entitiesIds.Contains(u.Id) && 
                                               u.Version == useCaseDiagram.Version)
                                   .Select(UseCaseViewModel.FromModel)
                                   .ToList();

            return UseCaseDiagramDetailedViewModel.FromModel(useCaseDiagram, useCases, actors);
        }
    }
}
