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
            ToTable("gsd_requirements.Actor");
            HasKey(a => new { a.id, a.locale });
        }
    }
}
