using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ActorMapping : EntityTypeConfiguration<Actor>
    {
        public ActorMapping()
        {
            ToTable("Actor");
            HasKey(a => a.Id);
            Property(e => e.Id).HasColumnName("id");

            HasRequired(e => e.UseCaseEntity).WithOptional();
        }
    }
}
