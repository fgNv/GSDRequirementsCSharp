namespace GSDRequirementsCSharp.Persistence
{
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gsd_requirements.Package")]
    public partial class Package : IEntity<Guid>
    {
        [Key]
        [Column(Order = 0)]
        public Guid id { get; set; }

        public Guid Id { get { return id; } }

        public Guid project_id { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        public Guid creator_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string locale { get; set; }

        public virtual Project Project { get; set; }
    }
}
