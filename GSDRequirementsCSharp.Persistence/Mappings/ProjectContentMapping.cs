using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class ProjectContentMapping : EntityTypeConfiguration<ProjectContent>
    {
        public ProjectContentMapping()
        {
            ToTable("ProjectContent");
            Property(pc => pc.Id).HasColumnName("id");
            Property(pc => pc.Locale).HasColumnName("locale");
            Property(pc => pc.ProjectId).HasColumnName("project_id");
            Property(pc => pc.Description).HasColumnName("description")
                                          .HasColumnType("text");
            Property(pc => pc.Id).HasColumnName("id");

            Property(p => p.Name).HasColumnName("name");
            Property(e => e.Name).IsUnicode(false);

            HasKey(pc => new { pc.Id, pc.Locale });

            Property(e => e.Description).IsUnicode(false);

            Property(e => e.Locale).IsUnicode(false);
        }
    }
}
