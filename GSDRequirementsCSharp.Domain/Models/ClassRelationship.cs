namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 
    
    public class ClassRelationship
    {
        public Guid Id { get; set; }

        [StringLength(10)]
        public string SourceMultiplicity { get; set; }

        [StringLength(10)]
        public string TargetMultiplicity { get; set; }

        public Guid SourceId { get; set; }

        public Guid TargetId { get; set; }

        public int Type { get; set; }
    }
}
