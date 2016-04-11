using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
            Property(e => e.Concluded).HasColumnName("concluded")
                                      .HasColumnAnnotation("Index",
                                                         new IndexAnnotation(new IndexAttribute("IX_Issue_Concluded")
                                                         {
                                                             IsUnique = false
                                                         })); ;
            Property(e => e.CreatorId).HasColumnName("creator_id");
            Property(i => i.SpecificationItemId).HasColumnName("specification_item_id");

            Property(i => i.CreatedAt).HasColumnName("created_at");
            Property(i => i.LastModification).HasColumnName("last_modification");
            Property(i => i.ConcludedAt).HasColumnName("concluded_at");

            Property(i => i.ProjectId).HasColumnName("project_id");
            Property(i => i.Identifier).HasColumnName("identifier");

            HasRequired(i => i.Project).WithMany()
                                       .HasForeignKey(i => i.ProjectId);
            
            HasMany(e => e.Contents).WithRequired(e => e.Issue);

            HasMany(e => e.IssueComments).WithRequired(e => e.Issue)
                                         .HasForeignKey(e => e.IssueId)
                                         .WillCascadeOnDelete(false);
        }
    }
}
