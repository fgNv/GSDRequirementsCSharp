using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GSDRequirementsCSharp.Persistence.Queries.UseCaseDiagrams.UseCaseById
{
    class UseCaseByIdQueryHandler : IQueryHandler<Guid, UseCaseViewModel>
    {
        private readonly GSDRequirementsContext _context;

        public UseCaseByIdQueryHandler(GSDRequirementsContext context)
        {
            _context = context;
        }

        public UseCaseViewModel Handle(Guid query)
        {
            var useCase = _context.UseCases
                                  .Include(uc => uc.Contents)
                                  .Include(uc => uc.PreConditions)
                                  .Include(uc => uc.PostConditions)
                                  .SingleOrDefault(uc => uc.Id == query &&
                                                         uc.Version == _context.UseCases
                                                                               .Where(uc1 => uc1.Id == query)
                                                                               .Max(uc1 => uc1.Version));
            return UseCaseViewModel.FromModel(useCase);
        }
    }
}
