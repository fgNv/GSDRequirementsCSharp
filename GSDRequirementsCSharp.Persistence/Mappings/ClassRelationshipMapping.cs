using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class ClassRelationshipMapping : EntityTypeConfiguration<ClassRelationship>
    {
        public ClassRelationshipMapping()
        {
            ToTable("ClassRelationship");

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.SourceMultiplicity).HasColumnName("source_multiplicity");
            Property(e => e.TargetMultiplicity).HasColumnName("target_multiplicity");
            Property(e => e.SourceId).HasColumnName("source_id");
            Property(e => e.TargetId).HasColumnName("target_id");
            Property(e => e.Type).HasColumnName("type");

            Property(e => e.SourceMultiplicity)
                .IsUnicode(false);

            Property(e => e.TargetMultiplicity)
                .IsUnicode(false);
        }
    }
}
