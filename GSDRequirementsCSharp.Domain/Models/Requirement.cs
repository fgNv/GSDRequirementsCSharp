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


        [Key, Column(Order = 0)]
        public Guid Id { get; set; }

        [Key, Column(Order = 1)]
        public int Version { get; set; }

        public bool IsLastVersion { get; set; }

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

        VersionKey IEntity<VersionKey>.Id
        {
            get
            {
                return new VersionKey { Id = Id, Version = Version };
            }
        }
    }
}
