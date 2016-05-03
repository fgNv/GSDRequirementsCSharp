using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseEntityMapping : EntityTypeConfiguration<UseCaseEntity>
    {
        public UseCaseEntityMapping()
        {
            ToTable("UseCaseEntity");
            HasKey(a => a.Id);
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Type).HasColumnName("type");

            Map<UseCase>(uc => uc.Requires(nameof(UseCaseEntity.Type))
                                 .HasValue((int)UseCaseEntityType.UseCase));
            Map<Actor>(uc => uc.Requires(nameof(UseCaseEntity.Type))
                                 .HasValue((int)UseCaseEntityType.Actor));
        }
    }
}
