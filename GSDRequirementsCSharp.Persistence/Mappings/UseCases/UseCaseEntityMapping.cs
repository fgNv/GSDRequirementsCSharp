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
        public const string TABLE_NAME = "UseCaseEntity";

        public UseCaseEntityMapping()
        {            
            ToTable(TABLE_NAME);
            HasKey(u => new { u.Id, u.Version });
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Version).HasColumnName("version");
            HasRequired(a => a.UseCaseDiagram).WithMany(cd => cd.Entities);            
        }
    }
}
