namespace GSDRequirementsCSharp.Domain
{
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCase")]
    public partial class UserCase
    {
        public Guid id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get; set; }

        public Guid? actor_id { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
