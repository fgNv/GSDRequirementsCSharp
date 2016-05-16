using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCasePreConditionContentMapping : EntityTypeConfiguration<UseCasePreConditionContent>
    {
        public UseCasePreConditionContentMapping()
        {
            HasKey(pc => new { pc.Id, pc.Locale });
            HasRequired(pc => pc.UseCasePreCondition).WithMany(p => p.Contents);

            Property(pc => pc.Id).HasColumnName("id");
            Property(pc => pc.Description).HasColumnName("description");
            Property(pc => pc.Locale).HasColumnName("locale");
        }
    }
}
