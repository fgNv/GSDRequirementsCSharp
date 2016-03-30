namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gsd_requirements.ClassDiagram")]
    public partial class ClassDiagram
    {
        [Key]
        [Column(Order = 0)]
        public Guid id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string locale { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
