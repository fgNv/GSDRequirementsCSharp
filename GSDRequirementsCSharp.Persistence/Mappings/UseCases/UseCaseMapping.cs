using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Persistence.Mappings.UseCases;
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
    class UseCaseMapping : EntityTypeConfiguration<UseCase>
    {
        public UseCaseMapping()
        {
            ToTable("UseCase");
            HasKey(u => new { u.Id, u.Version });
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Identifier).HasColumnName("identifier");
            Property(e => e.Version).HasColumnName("version");

            Property(e => e.IsLastVersionChar).HasColumnName("is_last_version");
            Ignore(e => e.IsLastVersion);

            Property(e => e.ProjectId).HasColumnName("project_id").HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("use_case_identifier", 2)
                {
                    IsUnique = true
                })); ;

            HasRequired(uc => uc.SpecificationItem).WithMany();

            HasRequired(uc => uc.Project).WithMany()
                                         .HasForeignKey(uc => uc.ProjectId);

            Property(p => p.Identifier).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("use_case_identifier", 1)
                {
                    IsUnique = true
                }));

            Property(p => p.Version).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("use_case_identifier", 3)
                {
                    IsUnique = true
                }));
        }
    }
}
