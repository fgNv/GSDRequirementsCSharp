using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    internal class SequenceDiagramContentMapping : EntityTypeConfiguration<SequenceDiagramContent>
    {
        public SequenceDiagramContentMapping()
        {
            ToTable("SequenceDiagramContent");

            HasKey(rc => new { rc.Id, rc.Locale, rc.Version });
            Property(rc => rc.Id).HasColumnName("id");
            Property(rc => rc.Locale).HasColumnName("locale");
            Property(rc => rc.Description).HasColumnName("description");            
            Property(rc => rc.CreatorId).HasColumnName("creator_id");
            Property(rc => rc.Version).HasColumnName("version");           

            HasOptional(sdc => sdc.Creator).WithMany()
                                           .HasForeignKey(sdc => sdc.CreatorId);
            Property(e => e.Locale).IsUnicode(false);
        }
    }
}
