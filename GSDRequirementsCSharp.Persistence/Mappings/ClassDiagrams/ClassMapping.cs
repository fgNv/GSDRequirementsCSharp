using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassMapping : EntityTypeConfiguration<Class>
    {
        public ClassMapping()
        {
            ToTable("Class");

            Property(c => c.Id).HasColumnName("id");
            Property(c => c.Type).HasColumnName("type");
            Property(c => c.Name).HasColumnName("name");
            Property(c => c.X).HasColumnName("coordinates_x");
            Property(c => c.Y).HasColumnName("coordinates_y");

            HasMany(e => e.ClassMethods)
                            .WithRequired(e => e.Class)
                            .HasForeignKey(e => e.ClassId)
                            .WillCascadeOnDelete(false);
            HasMany(e => e.ClassProperties)
                            .WithRequired(e => e.Class)
                            .HasForeignKey(e => e.ClassId)
                            .WillCascadeOnDelete(false);
        }
    }
}
