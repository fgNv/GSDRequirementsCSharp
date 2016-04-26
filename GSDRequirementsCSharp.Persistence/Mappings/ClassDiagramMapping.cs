using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassDiagramMapping : EntityTypeConfiguration<ClassDiagram>
    {
        public ClassDiagramMapping()
        {
            ToTable("ClassDiagram");
            Property(cd => cd.Id).HasColumnName("id");
            HasMany(cd => cd.Contents).WithRequired();
            Property(c => c.Version).HasColumnName("version");
            Property(c => c.Identifier).HasColumnName("identifier");
            Property(c => c.IsLastVersion).HasColumnName("is_last_version");

            Property(cd => cd.Active).HasColumnName("active");
            Property(cd => cd.ProjectId).HasColumnName("project_id");

            HasKey(cd => new { cd.Id, cd.Version });

            HasMany(cd => cd.Classes).WithRequired();

            HasMany(cd => cd.Relationships).WithRequired();

            HasRequired(cd => cd.Project).WithMany()
                                         .HasForeignKey(cd => cd.ProjectId);
        }
    }
}
