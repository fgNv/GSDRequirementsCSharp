using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.ContactId).HasColumnName("contactId");
            Property(u => u.Login).HasColumnName("login");
            Property(u => u.Password).HasColumnName("password");
            ToTable("User");

            Property(e => e.Login).IsUnicode(false);

            Property(e => e.Password).IsUnicode(false);

            HasMany(e => e.Issues).WithOptional(e => e.User)
            .HasForeignKey(e => e.creator_id);

            HasMany(e => e.IssueComments).WithRequired(e => e.User)
            .HasForeignKey(e => e.creator_id)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Projects).WithRequired(e => e.Owner)
             .HasForeignKey(e => e.OwnerId)
             .WillCascadeOnDelete(false);

            HasMany(e => e.Requirements).WithRequired(e => e.Creator)
            .HasForeignKey(e => e.CreatorId)
            .WillCascadeOnDelete(false);
        }
    }
}
