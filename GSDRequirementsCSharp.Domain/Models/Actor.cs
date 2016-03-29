namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Actor
    {
        public Guid id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }
        
        [StringLength(10)]
        public string locale { get; set; }
    }
}
