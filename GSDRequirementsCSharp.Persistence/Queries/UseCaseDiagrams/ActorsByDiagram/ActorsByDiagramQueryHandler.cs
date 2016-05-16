using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.UseCaseDiagrams;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.ActorsByDiagram
{
    class ActorsByDiagramQueryHandler : IQueryHandler<ActorsByDiagramQuery, IEnumerable<Actor>>
    {

        private readonly GSDRequirementsContext _context;

        public ActorsByDiagramQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public IEnumerable<Actor> Handle(ActorsByDiagramQuery query)
        {
            var actors = _context.Actors
                                 .Include(a => a.Contents)
                                 .Where(a => a.UseCaseDiagram.Id == query.DiagramId &&
                                               (!query.Version.HasValue ||
                                                 a.Version == query.Version.Value))
                                 .ToList();
            return actors;
        }
    }
}
