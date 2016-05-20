using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GSDRequirementsCSharp.Domain.Queries;

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
