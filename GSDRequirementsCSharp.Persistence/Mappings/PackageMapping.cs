using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class PackageMapping : EntityTypeConfiguration<Package>
    {
        public PackageMapping()
        {
            ToTable("Package");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("id");
            Property(p => p.ProjectId).HasColumnName("project_id");
            Property(p => p.Identifier).HasColumnName("identifier");

            Property(p => p.CreatorId).HasColumnName("creator_id");
            
            Property(p => p.Active).HasColumnName("active");
            HasMany(p => p.Contents).WithRequired(p => p.Package);

            Property(p => p.Identifier).HasColumnAnnotation(
                IndexAnnotation.AnnotationName, 
                new IndexAnnotation(new IndexAttribute("package_identifier", 1) {
                    IsUnique = true
                }));

            Property(p => p.ProjectId).HasColumnAnnotation(
                IndexAnnotation.AnnotationName, 
                new IndexAnnotation(new IndexAttribute("package_identifier", 2) {
                    IsUnique = true
                }));
        }
    }
}
