namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Issue : IEntity<Guid>
    {
        public Issue()
        {
            IssueComments = new HashSet<IssueComment>();
            Contents = new HashSet<IssueContent>();
        }

        public Guid Id { get; set; }

        public int Identifier { get; set; }

        public bool Concluded { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModification { get; set; }

        public DateTime? ConcludedAt { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }
                
        public ICollection<IssueContent> Contents { get; set; }

        public Guid SpecificationItemId { get; set; }

        public int? CreatorId { get; set; }

        public virtual User Creator { get; set; }
        
        public virtual ICollection<IssueComment> IssueComments { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
