using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class PermissionMapping : EntityTypeConfiguration<Permission>
    {
        public PermissionMapping()
        {
            ToTable("Permission");
            HasKey(p => p.Id);
            Property(e => e.ProjectId).HasColumnName("project_id");
            Property(e => e.Profile).HasColumnName("profile");
            Property(e => e.UserId).HasColumnName("user_id");

            HasRequired(e => e.User).WithMany(u => u.Permissions)
                                    .HasForeignKey(p => p.UserId);
        }
    }
}
