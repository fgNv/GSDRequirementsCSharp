using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseDiagramMapping : EntityTypeConfiguration<UseCaseDiagram>
    {
        public UseCaseDiagramMapping()
        {
            ToTable("UseCaseDiagram");
            HasKey(u => new { u.Id, u.Version });
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Version).HasColumnName("version");
            Property(e => e.SpecificationItemId).HasColumnName("specification_item_id");

            Property(e => e.ProjectId).HasColumnName("project_id");
            Property(e => e.IsLastVersion).HasColumnName("is_last_version");
            Property(e => e.Identifier).HasColumnName("identifier");
            
            HasRequired(e => e.SpecificationItem).WithMany(si => si.UseCaseDiagrams)
                                                 .HasForeignKey(ucd => ucd.SpecificationItemId);

            HasRequired(ucd => ucd.Project).WithMany()
                                           .HasForeignKey(ucd => ucd.ProjectId);
        }
    }
}
