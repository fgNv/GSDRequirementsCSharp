namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gsd_requirements.ClassPropertyContent")]
    public partial class ClassPropertyContent
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

        public virtual ClassProperty ClassProperty { get; set; }
    }
}
