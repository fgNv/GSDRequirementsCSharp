using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class RequirementMapping : EntityTypeConfiguration<Requirement>
    {
        public RequirementMapping()
        {
            ToTable("Requirement");
            HasKey(r => new { r.Id, r.Version });
            Property(r => r.Id).HasColumnName("id");
            Property(r => r.Difficulty).HasColumnName("difficulty");
            Property(r => r.Type).HasColumnName("type");
            Property(r => r.CreatorId).HasColumnName("creator_id");
            Property(r => r.ContactId).HasColumnName("contact_id");
            Property(r => r.Version).HasColumnName("version");
            Property(r => r.IsLastVersion).HasColumnName("is_last_version");

            HasMany(e => e.RequirementContents)
                .WithRequired(e => e.Requirement)
                .WillCascadeOnDelete(false);

            HasMany(e => e.RequirementRisks)
                .WithRequired(e => e.Requirement)
                .WillCascadeOnDelete(false);
        }
    }
}
