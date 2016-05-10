using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.ViewModels.UseCases;

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
                                       .Include(cd => cd.EntitiesRelations)
                                       .Include(cd => cd.UseCasesRelations)
                                       .Include(cd => cd.Contents)
                                       .SingleOrDefault(c => c.Id == id && c.IsLastVersion);

            if (useCaseDiagram == null)
                return null;


            var actors = _context.Actors
                                 .Include(u => u.Contents)
                                 .Where(u => u.UseCaseDiagram.Id == id)
                                 .Select(ActorViewModel.FromModel)
                                 .ToList();

            var useCases = _context.UseCases
                                   .Include(u => u.Contents)
                                   .Include(u => u.PreConditions.Select(pc => pc.Contents))
                                   .Include(u => u.PostConditions.Select(pc => pc.Contents))
                                   .Where(u => u.UseCaseDiagram.Id == id)
                                   .Select(UseCaseViewModel.FromModel)
                                   .ToList();

            return UseCaseDiagramDetailedViewModel.FromModel(useCaseDiagram, useCases, actors);
        }
    }
}
