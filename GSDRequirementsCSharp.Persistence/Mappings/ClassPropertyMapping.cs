using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassPropertyMapping : EntityTypeConfiguration<ClassProperty>
    {
        public ClassPropertyMapping()
        {
            ToTable("ClassProperty");
            Property(c => c.Id).HasColumnName("id");
            Property(c => c.ClassId).HasColumnName("class_id");
            Property(c => c.Visibility).HasColumnName("visibility");
            Property(c => c.Type).HasColumnName("type");

            Property(e => e.Type)
               .IsUnicode(false);

            HasMany(e => e.ClassPropertyContents)
                .WithRequired(e => e.ClassProperty)
                .WillCascadeOnDelete(false);
        }
    }
}
