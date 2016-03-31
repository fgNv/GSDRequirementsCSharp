namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassContent")]
    public partial class ClassContent
    {
        [Key]
        [Column(Order = 0)]
        public Guid id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string locale { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public virtual Class Class { get; set; }
    }
}
