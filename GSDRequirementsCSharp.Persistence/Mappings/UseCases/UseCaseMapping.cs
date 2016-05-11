using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class UseCaseMapping : EntityTypeConfiguration<UseCase>
    {
        public UseCaseMapping()
        {
            ToTable("UseCase");
            HasKey(u => u.Id);
            Property(e => e.Id).HasColumnName("id");
        }
    }
}
