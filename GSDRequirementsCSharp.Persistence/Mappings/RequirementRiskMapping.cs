using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class RequirementRiskMapping : EntityTypeConfiguration<RequirementRisk>
    {
        public RequirementRiskMapping()
        {
            ToTable("RequirementRisk");
            HasKey(rr => rr.Id);
            Property(rr => rr.Id).HasColumnName("id");
            Property(rr => rr.Description).HasColumnName("description")
                                          .HasColumnType("text");

            Property(e => e.Description).IsUnicode(false);
        }
    }
}
