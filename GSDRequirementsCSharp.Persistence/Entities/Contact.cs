namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gsd_requirements.Contact")]
    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
            Requirements = new HashSet<Requirement>();
            Users = new HashSet<User>();
        }

        public Guid id { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(20)]
        public string mobilePhone { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requirement> Requirements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
