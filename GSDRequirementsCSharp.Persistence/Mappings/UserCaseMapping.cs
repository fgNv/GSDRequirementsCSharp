using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class UserCaseMapping : EntityTypeConfiguration<UserCase>
    {
        public UserCaseMapping()
        {
            ToTable("UserCase");

            HasKey(u => u.Id);

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Name).HasColumnName("name");
            Property(e => e.Description).HasColumnName("description")
                                        .HasColumnType("text");
            Property(e => e.ActorId).HasColumnName("actor_id");

            Property(e => e.Name)
                .IsUnicode(false);

            Property(e => e.Description).IsUnicode(false);
        }
    }
}
