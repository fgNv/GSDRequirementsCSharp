using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Detailed
{
    internal class ClassDiagramDetailQueryHandler : IQueryHandler<ClassDiagramDetailQuery, ClassDiagramDetailedViewModel>,
                                                    IQueryHandler<ClassDiagramDetailQuery, ClassDiagram>
    {
        private readonly GSDRequirementsContext _context;

        public ClassDiagramDetailQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        private ClassDiagram Retrieve(ClassDiagramDetailQuery query)
        {
            return _context.ClassDiagrams
                                       .Include(cd => cd.Relationships)
                                       .Include(cd => cd.Classes.Select(c => c.ClassMethods))
                                       .Include(cd => cd.Contents)
                                       .SingleOrDefault(c => c.Id == query.Id &&
                                                             (c.IsLastVersion && !query.Version.HasValue ||
                                                             c.Version == query.Version.Value));
        }

        public ClassDiagramDetailedViewModel Handle(ClassDiagramDetailQuery query)
        {
            var classDiagram = Retrieve(query);

            if (classDiagram == null)
                return null;
            return ClassDiagramDetailedViewModel.FromModel(classDiagram);
        }

        ClassDiagram IQueryHandler<ClassDiagramDetailQuery, ClassDiagram>.Handle(ClassDiagramDetailQuery query)
        {
            return Retrieve(query);
        }
    }
}
