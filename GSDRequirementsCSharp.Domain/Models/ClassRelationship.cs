namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassRelationship")]
    public partial class ClassRelationship
    {
        public Guid id { get; set; }

        [StringLength(10)]
        public string source_multiplicity { get; set; }

        [StringLength(10)]
        public string target_multiplicity { get; set; }

        public Guid source_id { get; set; }

        public Guid target_id { get; set; }

        public int type { get; set; }
    }
}
