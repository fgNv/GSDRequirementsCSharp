using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Linq;
using System.Data.Entity;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Domain.ViewModels;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams
{
    class UseCaseBySpecificationItemIdQueryHandler : IQueryHandler<Guid, UseCaseViewModel>
    {
        private readonly GSDRequirementsContext _context;

        public UseCaseBySpecificationItemIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public UseCaseViewModel Handle(Guid query)
        {
            var useCase = _context.UseCases
                                  .Include(uc => uc.Contents)
                                  .Include(uc => uc.PreConditions.Select(pc => pc.Contents))
                                  .Include(uc => uc.PostConditions.Select(pc => pc.Contents))
                                  .SingleOrDefault(uc => uc.Id == query &&
                                                         uc.IsLastVersionChar == "True");

            if (useCase == null)
                throw new Exception(Sentences.useCaseNotFound);

            return UseCaseViewModel.FromModel(useCase);
        }
    }
}
