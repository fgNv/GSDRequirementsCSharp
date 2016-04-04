using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
            ToTable("Project");
            Property(p => p.Id).HasColumnName("id");
            HasKey(p => p.Id);
            Property(p => p.OwnerId).HasColumnName("owner_id");
            Property(p => p.CreatorId).HasColumnName("creator_id");
            Property(p => p.CreatedAt).HasColumnName("created_at");
            Property(p => p.Active).HasColumnName("active");
            Property(p => p.Identifier).HasColumnName("identifier");
            
            Property(p => p.Identifier).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("project_identifier", 1)
                {
                    IsUnique = true
                }));

            Property(p => p.CreatorId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("project_identifier", 2)
                {
                    IsUnique = true
                }));

            HasMany(e => e.Packages)
            .WithRequired(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Profiles)
            .WithRequired(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .WillCascadeOnDelete(false);

            HasMany(e => e.ProjectContents)
            .WithRequired(e => e.Project)
            .HasForeignKey(e => e.ProjectId)
            .WillCascadeOnDelete(false);
        }
    }
}
