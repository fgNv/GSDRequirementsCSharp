using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class IssueMapping : EntityTypeConfiguration<Issue>
    {
        public IssueMapping()
        {
            ToTable("Issue");

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Concluded).HasColumnName("concluded");
            Property(e => e.CreatorId).HasColumnName("creator_id");
            Property(e => e.Description).HasColumnName("description")
                                        .HasColumnType("text");            

            Property(e => e.Description)
                .IsUnicode(false);

            HasMany(e => e.IssueComments)
                .WithRequired(e => e.Issue)
                .HasForeignKey(e => e.IssueId)
                .WillCascadeOnDelete(false);
        }
    }
}
