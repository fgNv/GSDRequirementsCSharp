namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gsd_requirements.RequirementRisk")]
    public partial class RequirementRisk
    {
        public Guid id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get; set; }

        public Guid requirement_id { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}
