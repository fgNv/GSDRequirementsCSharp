using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassContentMapping : EntityTypeConfiguration<ClassContent>
    {
        public ClassContentMapping()
        {
            ToTable("ClassContent");
            HasKey(n => new { n.Id, n.Locale });
            Property(c => c.Id).HasColumnName("id");
            Property(c => c.Locale).HasColumnName("locale");
            Property(c => c.Name).HasColumnName("name");

            Property(e => e.Locale)
                .IsUnicode(false);

            Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
