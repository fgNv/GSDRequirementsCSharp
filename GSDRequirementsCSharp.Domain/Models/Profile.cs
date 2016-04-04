namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Profile : IEntity<Guid>
    {
        public Profile()
        {
            Users = new HashSet<User>();
        }
        
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual Project Project { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
