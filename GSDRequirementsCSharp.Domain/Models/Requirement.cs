namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Requirement : IEntity<Guid>
    {
        public Requirement()
        {
            RequirementContents = new HashSet<RequirementContent>();
            RequirementRisks = new HashSet<RequirementRisk>();
        }
        
        public Guid Id { get; set; }

        public int Rank { get; set; }

        public int Difficulty { get; set; }

        public int Type { get; set; }

        public Guid CreatorId { get; set; }

        public Guid ContactId { get; set; }

        public virtual Contact Contact { get; set; }
        
        public virtual ICollection<RequirementContent> RequirementContents { get; set; }

        public virtual User User { get; set; }
        
        public virtual ICollection<RequirementRisk> RequirementRisks { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }
    }
}
