namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IEntity<int>
    {
        public User()
        {
            Issues = new HashSet<Issue>();
            IssueComments = new HashSet<IssueComment>();
            Projects = new HashSet<Project>();
            Requirements = new HashSet<Requirement>();
            Permissions = new HashSet<Permission>();
        }
        
        public int Id { get; set; }

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
        
        public virtual ICollection<Permission> Permissions { get; set; }


    }
}
