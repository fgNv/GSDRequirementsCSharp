using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class UseCasePreConditionContent : IEntity<LocaleKey>
    {
        public Guid Id { get; set; }

        public string Locale { get; set; }

        public string Description { get; set; }
        
        public UseCasePreCondition UseCasePreCondition { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey { Id = Id, Locale = Locale };
            }
        }
    }
}
