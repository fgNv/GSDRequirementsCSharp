using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Infrastructure.Internationalization;

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
                                  .SingleOrDefault(uc => uc.SpecificationItemId == query &&
                                                         uc.IsLastVersionChar == "True");

            if (useCase == null)
                throw new Exception(Sentences.useCaseNotFound);

            return UseCaseViewModel.FromModel(useCase);
        }
    }
}
