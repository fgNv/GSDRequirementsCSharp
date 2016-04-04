using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ContactMapping : EntityTypeConfiguration<Contact>
    {
        public ContactMapping()
        {
            ToTable("Contact");
            Property(e => e.Email).HasColumnName("email");
            Property(e => e.MobilePhone).HasColumnName("mobilePhone");
            Property(e => e.Name).HasColumnName("name");
            Property(e => e.Phone).HasColumnName("phone");
            Property(e => e.Id).HasColumnName("id");

            Property(e => e.Email)
                .IsUnicode(false);

            Property(e => e.MobilePhone)
                .IsUnicode(false);

            Property(e => e.Name)
                .IsUnicode(false);

            Property(e => e.Phone)
                .IsUnicode(false);

            HasMany(e => e.Users)
                .WithRequired(e => e.Contact)
                .WillCascadeOnDelete(false);
        }
    }
}
