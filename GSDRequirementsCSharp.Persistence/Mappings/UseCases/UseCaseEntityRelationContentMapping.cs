using GSDRequirementsCSharp.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseEntityRelationContentMapping : EntityTypeConfiguration<UseCaseEntityRelationContent>
    {
        public UseCaseEntityRelationContentMapping()
        {
            ToTable("UseCaseEntitiesRelationContent");
            HasKey(a => new { a.Id, a.Locale });
            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Locale).HasColumnName("locale");
            Property(e => e.Description).HasColumnName("description");
            HasRequired(a => a.UseCaseEntityRelation).WithMany(cd => cd.Contents)
                                                     .HasForeignKey(a => a.Id);
        }
    }
}
