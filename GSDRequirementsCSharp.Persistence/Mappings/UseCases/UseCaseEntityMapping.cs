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
            HasRequired(a => a.UseCaseDiagram).WithMany(cd => cd.Entities);

            //HasOptional(uc => uc.Actor).WithRequired(a => a.UseCaseEntity);
            //HasOptional(uc => uc.UseCase).WithRequired(a => a.UseCaseEntity);
        }
    }
}
