using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Persistence.Mappings.UseCases;
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
            HasKey(u => new { u.Id, u.Version });
            Property(e => e.Version).HasColumnName("version");
            Property(e => e.Id).HasColumnName("id");          
        }
    }
}
