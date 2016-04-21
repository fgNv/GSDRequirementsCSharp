namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Contact : IEntity<Guid>
    {
        public Contact()
        {
            Requirements = new HashSet<Requirement>();
            Users = new HashSet<User>();
        }
        
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }
        
        public virtual ICollection<Requirement> Requirements { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
