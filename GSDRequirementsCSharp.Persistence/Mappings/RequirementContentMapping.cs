using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class RequirementContentMapping : EntityTypeConfiguration<RequirementContent>
    {
        public RequirementContentMapping()
        {
            HasKey(rc => new { rc.Id, rc.Locale, rc.Version });
            Property(rc => rc.Id).HasColumnName("id");
            Property(rc => rc.Locale).HasColumnName("locale");
            Property(rc => rc.Action).HasColumnName("action");
            Property(rc => rc.Condition).HasColumnName("condition");
            Property(rc => rc.Subject).HasColumnName("subject");
            Property(rc => rc.CreatorId).HasColumnName("creator_id");
            Property(rc => rc.Version).HasColumnName("version");
            ToTable("RequirementContent");

            Property(e => e.Locale).IsUnicode(false);
            Property(e => e.Action).IsUnicode(false);
            Property(e => e.Condition).IsUnicode(false);
            Property(e => e.Subject).IsUnicode(false);
        }
    }
}
