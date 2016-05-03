using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class UseCaseMapping : EntityTypeConfiguration<UseCase>
    {
        public UseCaseMapping()
        {
            ToTable("UseCase");
            HasKey(u => u.Id);
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Type).HasColumnName("type");
            Property(e => e.UseCaseDiagramId).HasColumnName("use_case_diagram_id");

            HasRequired(e => e.UseCaseDiagram).WithMany(ucd => ucd.UseCases)
                                              .HasForeignKey(uc => uc.UseCaseDiagramId);

        }
    }
}
