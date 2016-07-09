using GSDRequirementsCSharp.Domain.Models;
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
    internal class SequenceDiagramMapping : EntityTypeConfiguration<SequenceDiagram>
    {
        public SequenceDiagramMapping()
        {
            ToTable("SequenceDiagram");
            HasKey(r => new { r.Id, r.Version });
            Property(r => r.Id).HasColumnName("id");
            
            Property(r => r.Version).HasColumnName("version");
            Property(r => r.IsLastVersion).HasColumnName("is_last_version")
                                          .HasColumnAnnotation("Index",
                                                         new IndexAnnotation(new IndexAttribute("IX_Requirement_Last_Version")
                                                         {
                                                             IsUnique = false
                                                         })); ;
            Property(r => r.Identifier).HasColumnName("identifier")
                                       .HasColumnAnnotation("Index",
                                                         new IndexAnnotation(new IndexAttribute("IX_Requirement_Identifier")
                                                         {
                                                             IsUnique = false
                                                         }));
            Property(r => r.ProjectId).HasColumnName("project_id");

            HasRequired(si => si.SpecificationItem).WithMany()
                                                   .HasForeignKey(cd => cd.Id);

            HasRequired(r => r.Project).WithMany()
                                       .HasForeignKey(p => p.ProjectId);

            Property(p => p.Identifier).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("requirement_identifier", 1)
                {
                    IsUnique = true
                }));
            
            Property(p => p.ProjectId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("requirement_identifier", 3)
                {
                    IsUnique = true
                }));

            Property(p => p.Version).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("requirement_identifier", 4)
                {
                    IsUnique = true
                }));
            
            HasMany(e => e.Contents)
                .WithRequired(e => e.SequenceDiagram)
                .WillCascadeOnDelete(false);
        }
    }
}
