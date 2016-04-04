using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassMethodContentMapping : EntityTypeConfiguration<ClassMethodContent>
    {
        public ClassMethodContentMapping()
        {
            ToTable("ClassMethodContent");
            HasKey(cm => new { cm.Id, cm.Locale });
            Property(cm => cm.Id).HasColumnName("id");
            Property(cm => cm.Name).HasColumnName("name");

            Property(e => e.Locale)
                            .IsUnicode(false);
            Property(e => e.Name)
                            .IsUnicode(false);
        }
    }
}
