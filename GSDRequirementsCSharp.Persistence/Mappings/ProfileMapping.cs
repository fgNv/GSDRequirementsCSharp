using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class ProfileMapping : EntityTypeConfiguration<Profile>
    {
        public ProfileMapping()
        {
            ToTable("Profile");
            HasKey(p => p.Id);
            Property(e => e.ProjectId).HasColumnName("project_id");
            Property(e => e.Name).HasColumnName("name");

            Property(e => e.Name)
                .IsUnicode(false);

            HasMany(e => e.Users)
                .WithMany(e => e.Profiles)
                .Map(m => m.ToTable("User_Profile").MapLeftKey("profile_id").MapRightKey("user_id"));
        }
    }
}
