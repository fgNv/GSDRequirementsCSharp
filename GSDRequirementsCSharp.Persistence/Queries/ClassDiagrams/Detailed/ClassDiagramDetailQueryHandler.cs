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
    internal class ClassDiagramDetailQueryHandler : IQueryHandler<ClassDiagramDetailQuery, ClassDiagramDetailedViewModel>
    {
        private readonly GSDRequirementsContext _context;

        public ClassDiagramDetailQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public ClassDiagramDetailedViewModel Handle(ClassDiagramDetailQuery query)
        {
            var classDiagram = _context.ClassDiagrams
                                       .Include(cd => cd.Relationships)
                                       .Include(cd => cd.Classes.Select(c => c.ClassMethods))
                                       .Include(cd => cd.Contents)
                                       .SingleOrDefault(c => c.Id == query.Id && 
                                                             (c.IsLastVersion && !query.Version.HasValue ||
                                                             c.Version == query.Version.Value));

            if (classDiagram == null)
                return null;
            return ClassDiagramDetailedViewModel.FromModel(classDiagram);
        }
    }
}
