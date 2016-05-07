using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models.UseCases
{
    public class UseCasePreCondition : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public ICollection<UseCasePreConditionContent> Contents { get; set; }

        public UseCase UseCase { get; set; }

        public UseCasePreCondition()
        {
            Contents = new HashSet<UseCasePreConditionContent>();
        }
    }
}
