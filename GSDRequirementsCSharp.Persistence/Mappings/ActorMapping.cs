using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class ActorMapping : EntityTypeConfiguration<Actor>
    {
        public ActorMapping()
        {
            ToTable("Actor");
            HasKey(a => new { a.id, a.locale });

            Property(e => e.name)
               .IsUnicode(false);

            Property(e => e.locale)
                .IsUnicode(false);
        }
    }
}
