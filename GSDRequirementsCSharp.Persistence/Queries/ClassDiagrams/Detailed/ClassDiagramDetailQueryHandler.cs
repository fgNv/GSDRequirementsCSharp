using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Detailed
{
    internal class ClassDiagramDetailQueryHandler : IQueryHandler<Guid, ClassDiagramDetailedViewModel>
    {
        private readonly GSDRequirementsContext _context;

        public ClassDiagramDetailQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public ClassDiagramDetailedViewModel Handle(Guid id)
        {
            var classDiagram = _context.ClassDiagrams
                                       .Include(cd => cd.Relationships)
                                       .Include(cd => cd.Classes.Select(c => c.ClassMethods))
                                       .Include(cd => cd.Contents)
                                       .FirstOrDefault(c => c.Id == id);

            if (classDiagram == null)
                return null;
            return ClassDiagramDetailedViewModel.FromModel(classDiagram);
        }
    }
}
