using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
