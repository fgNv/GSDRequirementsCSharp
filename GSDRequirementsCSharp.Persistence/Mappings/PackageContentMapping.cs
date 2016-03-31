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

            HasKey(p => new { p.Id, p.Locale });
            Property(p => p.Description).HasColumnName("description")
                                        .HasColumnType("text")
                                        .HasMaxLength(65535);
            Property(p => p.Locale).HasColumnName("locale")
                                   .HasMaxLength(10);
            Property(e => e.Description).IsUnicode(false);
            Property(e => e.Locale).IsUnicode(false);
            HasRequired(p => p.Package);
        }
    }
}
