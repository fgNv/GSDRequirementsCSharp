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
            Property(e => e.Type).HasColumnName("type");            
            Property(e => e.UseCaseDiagramId).HasColumnName("use_case_diagram_id");
            HasRequired(a => a.UseCaseDiagram).WithMany(cd => cd.Actors)
                                              .HasForeignKey(a => a.UseCaseDiagramId);
        }
    }
}
