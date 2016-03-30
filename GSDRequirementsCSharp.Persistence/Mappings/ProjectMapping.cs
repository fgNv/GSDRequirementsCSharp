using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class ProjectMapping : EntityTypeConfiguration<Project>
    {
        public ProjectMapping()
        {
            ToTable("gsd_requirements.Project");
            Property(p => p.Id).HasColumnName("id");
            HasKey(p => p.Id);
            Property(p => p.Name).HasColumnName("name");
            Property(p => p.OwnerId).HasColumnName("owner_id");
            Property(p => p.CreatorId).HasColumnName("creator_id");
            Property(p => p.CreatedAt).HasColumnName("created_at");

            Property(e => e.Name).IsUnicode(false);
            
            HasMany(e => e.Packages)
            .WithRequired(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Profiles)
            .WithRequired(e => e.Project)
            .HasForeignKey(e => e.project_id)
            .WillCascadeOnDelete(false);

            HasMany(e => e.ProjectContents)
            .WithRequired(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .WillCascadeOnDelete(false);
        }
    }
}
