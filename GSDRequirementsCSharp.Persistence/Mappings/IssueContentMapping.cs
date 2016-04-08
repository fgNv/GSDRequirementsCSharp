using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class IssueContentMapping : EntityTypeConfiguration<IssueContent>
    {
        public IssueContentMapping()
        {
            ToTable("IssueContent");
            
            Property(e => e.Description).HasColumnName("description")
                                    .HasColumnType("text");

            Property(e => e.Description).IsUnicode(false);

            Property(p => p.Id).HasColumnName("id");
            Property(p => p.Id).HasColumnOrder(0);

            Property(p => p.IsUpdated).HasColumnName("is_updated");

            Property(p => p.Locale).HasColumnName("locale");
            Property(p => p.Locale).HasMaxLength(10);
            Property(p => p.Locale).HasColumnOrder(1);
            Property(p => p.Locale).IsRequired();

            HasKey(p => new { p.Id, p.Locale });

            HasRequired(p => p.Issue);
        }
    }
}
