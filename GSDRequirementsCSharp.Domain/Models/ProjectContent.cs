namespace GSDRequirementsCSharp.Persistence
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gsd_requirements.ProjectContent")]
    public partial class ProjectContent : IEntity<Guid>
    {

        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string locale { get; set; }

        public Guid project_id { get; set; }

        public virtual Project Project { get; set; }
    }
}
