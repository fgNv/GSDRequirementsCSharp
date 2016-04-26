namespace GSDRequirementsCSharp.Domain
{
    using Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ClassRelationship
    {
        public Guid Id { get; set; }

        [StringLength(10)]
        public string SourceMultiplicity { get; set; }

        [StringLength(10)]
        public string TargetMultiplicity { get; set; }
        
        public Guid SourceId { get; set; }

        public Guid TargetId { get; set; }

        public RelationType Type { get; set; }
    }
}
