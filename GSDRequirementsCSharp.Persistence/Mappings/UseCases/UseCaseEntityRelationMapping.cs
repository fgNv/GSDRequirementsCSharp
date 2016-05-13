using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseEntityRelationMapping : EntityTypeConfiguration<UseCaseEntityRelation>
    {
        public UseCaseEntityRelationMapping()
        {
            ToTable("UseCaseEntityRelation");
            HasKey(a => a.Id);
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.UseCaseDiagramId).HasColumnName("use_case_diagram_id");

            HasRequired(r => r.UseCaseDiagram).WithMany(ucd => ucd.EntitiesRelations);

            HasRequired(e => e.Source).WithMany();
            HasRequired(e => e.Target).WithMany();
        }
    }
}
