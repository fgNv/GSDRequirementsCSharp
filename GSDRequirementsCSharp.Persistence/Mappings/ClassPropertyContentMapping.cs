using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassPropertyContentMapping : EntityTypeConfiguration<ClassPropertyContent>
    {
        public ClassPropertyContentMapping()
        {
            ToTable("ClassPropertyContent");
            HasKey(c => new { c.Id, c.Locale });
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Name).HasColumnName("name");
            Property(e => e.Locale).HasColumnName("locale");

            Property(e => e.Locale).IsUnicode(false);
            
            Property(e => e.Name).IsUnicode(false);
        }
    }
}
