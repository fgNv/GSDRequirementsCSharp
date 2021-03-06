namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Requirement : IEntity<VersionKey>
    {
        public Requirement()
        {
            RequirementContents = new HashSet<RequirementContent>();
            RequirementRisks = new HashSet<RequirementRisk>();
        }
                
        public Guid Id { get; set; }
        
        public int Version { get; set; }

        public int Identifier { get; set; }

        public bool IsLastVersion { get; set; }

        public int Rank { get; set; }

        public Difficulty Difficulty { get; set; }

        public RequirementType Type { get; set; }

        public virtual Guid? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public virtual ICollection<RequirementContent> RequirementContents { get; set; }

        public virtual User Creator { get; set; }

        public int CreatorId { get; set; }

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
