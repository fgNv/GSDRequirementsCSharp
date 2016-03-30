using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class PackageMapping : EntityTypeConfiguration<Package>
    {
        public PackageMapping()
        {
            ToTable("gsd_requirements.Package");
            HasKey(p => new { p.Id, p.Locale });
            Property(p => p.Id).HasColumnName("id");
            Property(p => p.ProjectId).HasColumnName("project_id");
            Property(p => p.Description).HasColumnName("description")
                                        .HasColumnType("text")
                                        .HasMaxLength(65535);
            Property(p => p.CreatorId).HasColumnName("creator_id");
            Property(p => p.Locale).HasColumnName("locale")
                                   .HasMaxLength(10);

            Property(e => e.Description).IsUnicode(false);

            Property(e => e.Locale).IsUnicode(false);
        }
    }
}
