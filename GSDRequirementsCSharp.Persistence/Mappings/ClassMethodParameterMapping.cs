using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassMethodParameterMapping : EntityTypeConfiguration<ClassMethodParameter>
    {
        public ClassMethodParameterMapping()
        {
            ToTable("ClassMethodParameter");
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.ClassMethodId).HasColumnName("class_method_id");
            Property(e => e.Type).HasColumnName("type");
            Property(c => c.Name).HasColumnName("name");

            Property(e => e.Type)
                            .IsUnicode(false);
        }
    }
}
