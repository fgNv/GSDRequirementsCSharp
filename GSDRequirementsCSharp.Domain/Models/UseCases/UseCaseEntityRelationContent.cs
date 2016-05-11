using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models.UseCases
{
    public class UseCaseEntityRelationContent : IEntity<LocaleKey>
    {
        public Guid Id { get; set; }

        public string Locale { get; set; }

        public string Description { get; set; }

        public UseCaseEntityRelation UseCaseEntityRelation { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey { Id = Id, Locale = Locale };
            }
        }
    }
}
