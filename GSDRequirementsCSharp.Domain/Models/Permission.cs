namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Permission : IEntity<Guid>
    {        
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        
        public Profile Profile { get; set; }

        public virtual Project Project { get; set; }
        
        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
