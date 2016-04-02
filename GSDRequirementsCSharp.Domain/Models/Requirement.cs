namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Requirement : IEntity<VersionKey>
    {
        public Requirement()
        {
            RequirementContents = new HashSet<RequirementContent>();
            RequirementRisks = new HashSet<RequirementRisk>();
        }
                
        public Guid Id { get; set; }
        
        public int Version { get; set; }

        public bool IsLastVersion { get; set; }

        public int Rank { get; set; }

        public Difficulty Difficulty { get; set; }

        public RequirementType Type { get; set; }

        public Guid CreatorId { get; set; }

        public virtual Guid? ContactId { get; set; }

        public virtual Contact Contact { get; set; }
        
        public virtual ICollection<RequirementContent> RequirementContents { get; set; }

        public virtual User User { get; set; }
        
        public virtual ICollection<RequirementRisk> RequirementRisks { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }

        VersionKey IEntity<VersionKey>.Id
        {
            get
            {
                return new VersionKey { Id = Id, Version = Version };
            }
        }
    }
}
