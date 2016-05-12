using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseDiagramContentMapping : EntityTypeConfiguration<UseCaseDiagramContent>
    {
        public UseCaseDiagramContentMapping()
        {
            ToTable("UseCaseDiagramContent");
            HasKey(e => new { e.Id, e.Locale});

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Name).HasColumnName("name");
            Property(e => e.Locale).HasColumnName("locale");
            
            HasRequired(e => e.UseCaseDiagram).WithMany(ucd => ucd.Contents);
        }
    }
}
