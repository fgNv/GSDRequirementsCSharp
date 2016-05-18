using GSDRequirementsCSharp.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ActorContentMapping : EntityTypeConfiguration<ActorContent>
    {
        public ActorContentMapping()
        {
            ToTable("ActorContent");
            HasKey(a => new { a.Id, a.Locale });

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.Name).HasColumnName("name");            
            Property(e => e.Locale).HasColumnName("locale");

            HasRequired(e => e.Actor).WithMany(a => a.Contents);

            Property(e => e.Name)
               .IsUnicode(false);

            Property(e => e.Locale)
                .IsUnicode(false);
        }
    }
}
