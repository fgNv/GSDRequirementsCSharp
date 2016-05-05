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
                                       .Include(cd => cd.Entities)
                                       .Include(cd => cd.Relations)
                                       .Include(cd => cd.UseCaseRelations)
                                       .Include(cd => cd.Contents)
                                       .SingleOrDefault(c => c.Id == id && c.IsLastVersion);

            if (useCaseDiagram == null)
                return null;

            return UseCaseDiagramDetailedViewModel.FromModel(useCaseDiagram);
        }
    }
}
