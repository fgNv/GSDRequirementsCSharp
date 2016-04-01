using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
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
            Property(si => si.Active).HasColumnName("active");
            HasRequired(si => si.Package);

            HasMany(e => e.Issues)
                            .WithRequired(e => e.SpecificationItem)
                            .HasForeignKey(e => e.specification_item_id)
                            .WillCascadeOnDelete(false);

            HasMany(e => e.ClassDiagrams)
                            .WithRequired(e => e.SpecificationItem);

            HasMany(e => e.Requirements)
                            .WithRequired(e => e.SpecificationItem);

            HasMany(e => e.UserCases)
                            .WithRequired(e => e.SpecificationItem);
        }
    }
}
