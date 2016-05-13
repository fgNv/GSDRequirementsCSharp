using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    public class UseCasesRelationMapping : EntityTypeConfiguration<UseCasesRelation>
    {
        public UseCasesRelationMapping()
        {
            ToTable("UseCasesRelation");
            HasKey(a => a.Id);
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Type).HasColumnName("type");

            Property(e => e.UseCaseDiagramId).HasColumnName("use_case_diagram_id");

            Property(e => e.TargetId).HasColumnName("target_id");
            Property(e => e.SourceId).HasColumnName("source_id");
            Property(e => e.Version).HasColumnName("version");

            HasRequired(r => r.UseCaseDiagram).WithMany(ucd => ucd.UseCasesRelations)
                                              .HasForeignKey(r => new
                                              {
                                                  r.UseCaseDiagramId,
                                                  r.Version
                                              } );

            HasRequired(e => e.Source).WithMany()
                                      .HasForeignKey(e => new { e.SourceId, e.Version });
            HasRequired(e => e.Target).WithMany()
                                      .HasForeignKey(e => new { e.TargetId, e.Version});
        }
    }
}
