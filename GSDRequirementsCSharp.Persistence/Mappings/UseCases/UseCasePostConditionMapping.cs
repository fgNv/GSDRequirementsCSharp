using GSDRequirementsCSharp.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCasePostConditionMapping : EntityTypeConfiguration<UseCasePostCondition>
    {
        public UseCasePostConditionMapping()
        {
            HasKey(pc => pc.Id);
            Property(pc => pc.Id).HasColumnName("id");
            HasRequired(p => p.UseCase).WithMany(uc => uc.PostConditions);
        }
    }
}
