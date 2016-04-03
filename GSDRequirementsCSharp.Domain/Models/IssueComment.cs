namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IssueComment")]
    public partial class IssueComment
    {
        public Guid id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string content { get; set; }

        public Guid issue_id { get; set; }

        public int creator_id { get; set; }

        public virtual Issue Issue { get; set; }

        public virtual User User { get; set; }
    }
}
