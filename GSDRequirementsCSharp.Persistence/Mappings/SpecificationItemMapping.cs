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
    public class SpecificationItemMapping : EntityTypeConfiguration<SpecificationItem>
    {
        public SpecificationItemMapping()
        {
            ToTable("SpecificationItem");

            HasKey(si => si.Id);

            Property(si => si.Id).HasColumnName("id");
            Property(si => si.PackageId).HasColumnName("package_id");
            Property(si => si.Active).HasColumnName("active")
                                    .HasColumnAnnotation("Index", 
                                                         new IndexAnnotation(new IndexAttribute("IX_Specification_Active") {
                                                             IsUnique = false
                                                         }));
            Property(si => si.Type).HasColumnName("type");
            Property(si => si.Label).HasColumnName("label");

            HasRequired(si => si.Package);

            HasMany(e => e.Issues)
                            .WithRequired(e => e.SpecificationItem)
                            .HasForeignKey(e => e.SpecificationItemId)
                            .WillCascadeOnDelete(false);
            
            HasMany(e => e.LinkedItems).WithMany();
        }
    }
}
