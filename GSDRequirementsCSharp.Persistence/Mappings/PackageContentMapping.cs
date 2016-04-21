using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class PackageContentMapping : EntityTypeConfiguration<PackageContent>
    {
        public PackageContentMapping()
        {
            ToTable("PackageContent");

            Property(p => p.Description).HasColumnName("description")
                                        .HasColumnType("text")
                                        .HasMaxLength(65535);

            Property(p => p.Id).HasColumnName("id");
            Property(p => p.Id).HasColumnOrder(0);

            Property(p => p.IsUpdated).HasColumnName("is_updated");

            Property(p => p.Locale).HasColumnName("locale");
            Property(p => p.Locale).HasMaxLength(10);
            Property(p => p.Locale).HasColumnOrder(1);
            Property(p => p.Locale).IsRequired();

            HasKey(p => new { p.Id, p.Locale });

            Property(e => e.Description).IsUnicode(false);
            Property(e => e.Locale).IsUnicode(false);
            HasRequired(p => p.Package);
        }
    }
}
