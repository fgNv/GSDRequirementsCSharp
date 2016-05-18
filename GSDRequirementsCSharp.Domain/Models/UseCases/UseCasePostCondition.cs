using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class UseCasePostCondition : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public ICollection<UseCasePostConditionContent> Contents { get; set; }
        
        public UseCase UseCase { get; set; }

        public UseCasePostCondition()
        {
            Contents = new HashSet<UseCasePostConditionContent>();
        }
    }
}
