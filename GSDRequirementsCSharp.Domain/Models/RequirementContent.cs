namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequirementContent")]
    public partial class RequirementContent
    {
        [Key]
        [Column(Order = 0)]
        public Guid id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string locale { get; set; }

        [StringLength(150)]
        public string action { get; set; }

        [StringLength(150)]
        public string condition { get; set; }

        [StringLength(150)]
        public string subject { get; set; }

        public Guid? creator_id { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}
