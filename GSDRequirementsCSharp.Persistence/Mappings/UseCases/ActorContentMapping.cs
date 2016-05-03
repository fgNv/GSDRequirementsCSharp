using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ActorContentMapping : EntityTypeConfiguration<ActorContent>
    {
        public ActorContentMapping()
        {
            ToTable("ActorContent");
            HasKey(a => new { a.Id, a.Locale });

            Property(e => e.Name)
               .IsUnicode(false);

            Property(e => e.Locale)
                .IsUnicode(false);
        }
    }
}
