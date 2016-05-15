using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class AuditingMapping : EntityTypeConfiguration<Auditing>
    {
        public AuditingMapping()
        {
            ToTable("Auditing");

            HasKey(a => a.Id);
            Property(a => a.Id).HasColumnName("id");
            Property(a => a.ProjectId).HasColumnName("project_id");
            HasRequired(a => a.Project).WithMany().HasForeignKey(a => a.ProjectId);

            Property(a => a.UserId).HasColumnName("user_id");
            HasRequired(a => a.User).WithMany().HasForeignKey(a => a.UserId);

            Property(a => a.ActivityDescription).HasColumnName("activity_description");
            Property(a => a.ExecutedAt).HasColumnName("executed_at");
        }
    }
}
