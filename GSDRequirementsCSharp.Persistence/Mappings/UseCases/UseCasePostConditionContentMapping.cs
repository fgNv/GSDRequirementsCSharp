using GSDRequirementsCSharp.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCasePostConditionContentMapping : EntityTypeConfiguration<UseCasePostConditionContent>
    {
        public UseCasePostConditionContentMapping()
        {
            HasKey(pc => new { pc.Id, pc.Locale });
            HasRequired(pc => pc.UseCasePostCondition).WithMany(p => p.Contents);

            Property(pc => pc.Id).HasColumnName("id");
            Property(pc => pc.Description).HasColumnName("description");
            Property(pc => pc.Locale).HasColumnName("locale");
        }
    }
}
