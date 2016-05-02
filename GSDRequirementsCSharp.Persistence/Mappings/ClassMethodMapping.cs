using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassMethodMapping : EntityTypeConfiguration<ClassMethod>
    {
        public ClassMethodMapping()
        {
            ToTable("ClassMethod");
            Property(cm => cm.Id).HasColumnName("id");
            Property(cm => cm.ReturnType).HasColumnName("return_type");
            Property(cm => cm.Visibility).HasColumnName("visibility");
            Property(cm => cm.ClassId).HasColumnName("class_id");
            Property(c => c.Name).HasColumnName("name");

            Property(e => e.ReturnType)
                            .IsUnicode(false);
            HasMany(e => e.ClassMethodParameters)
                            .WithRequired(e => e.ClassMethod)
                            .HasForeignKey(e => e.ClassMethodId)
                            .WillCascadeOnDelete(false);
        }
    }
}
