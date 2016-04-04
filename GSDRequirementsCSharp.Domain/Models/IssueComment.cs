namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class IssueComment
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(65535)]
        public string Content { get; set; }

        public Guid IssueId { get; set; }

        public int CreatorId { get; set; }

        public virtual Issue Issue { get; set; }

        public virtual User Creator { get; set; }
    }
}
