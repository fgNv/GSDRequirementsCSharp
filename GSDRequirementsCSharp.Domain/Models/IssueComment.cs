namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class IssueComment : IEntity <Guid>
    {
        public Guid Id { get; set; }

        public ICollection<IssueCommentContent> Contents { get; set; }

        public Guid IssueId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Issue Issue { get; set; }

        public virtual User Creator { get; set; }
    }
}
