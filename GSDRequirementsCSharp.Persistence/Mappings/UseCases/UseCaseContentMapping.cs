using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseContentMapping : EntityTypeConfiguration<UseCaseContent>
    {
        public UseCaseContentMapping()
        {
            ToTable("UseCaseContent");

            HasKey(u => new { u.Id, u.Locale });

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Name).HasColumnName("name");
            Property(e => e.Description).HasColumnName("description")
                                        .HasColumnType("text");

            Property(e => e.Locale).HasColumnName("locale");
            HasRequired(e => e.UseCase).WithMany(uc => uc.Contents);

            Property(e => e.Name)
                .IsUnicode(false);

            Property(e => e.Description).IsUnicode(false);
        }
    }
}
