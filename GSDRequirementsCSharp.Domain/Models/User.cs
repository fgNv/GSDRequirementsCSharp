namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class User : IEntity<Guid>
    {
        public User()
        {
            Issues = new HashSet<Issue>();
            IssueComments = new HashSet<IssueComment>();
            Projects = new HashSet<Project>();
            Requirements = new HashSet<Requirement>();
            Profiles = new HashSet<Profile>();
        }
        
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public Guid ContactId { get; set; }

        public virtual Contact Contact { get; set; }
        
        public virtual ICollection<Issue> Issues { get; set; }
        
        public virtual ICollection<IssueComment> IssueComments { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; }
        
        public virtual ICollection<Requirement> Requirements { get; set; }
        
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
