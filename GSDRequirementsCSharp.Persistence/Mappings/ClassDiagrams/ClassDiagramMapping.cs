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
    class ClassDiagramMapping : EntityTypeConfiguration<ClassDiagram>
    {
        public ClassDiagramMapping()
        {
            ToTable("ClassDiagram");
            Property(cd => cd.Id).HasColumnName("id");
            HasMany(cd => cd.Contents).WithRequired();
            Property(c => c.Version).HasColumnName("version");
            Property(c => c.Identifier).HasColumnName("identifier");
            Property(c => c.IsLastVersion).HasColumnName("is_last_version");

            Property(cd => cd.ProjectId).HasColumnName("project_id")
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("class_diagram_identifier", 2)
                {
                    IsUnique = true
                })); ;

            HasKey(cd => new { cd.Id, cd.Version });

            HasMany(cd => cd.Classes).WithRequired();

            HasMany(cd => cd.Relationships).WithRequired();

            HasRequired(si => si.SpecificationItem).WithMany()
                                                   .HasForeignKey(cd => cd.Id);

            Property(p => p.Identifier).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("class_diagram_identifier", 1)
                {
                    IsUnique = true
                }));

            Property(p => p.Version).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("class_diagram_identifier", 3)
                {
                    IsUnique = true
                }));

            HasRequired(cd => cd.Project).WithMany()
                                         .HasForeignKey(cd => cd.ProjectId);
        }
    }
}
