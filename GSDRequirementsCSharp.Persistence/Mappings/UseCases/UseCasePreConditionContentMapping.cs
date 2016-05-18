using GSDRequirementsCSharp.Domain.Models;
using System.Data.Entity.ModelConfiguration;

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
