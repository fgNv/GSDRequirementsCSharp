namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project : IEntity<Guid>
    {
        public Project()
        {
            Packages = new HashSet<Package>();
            Profiles = new HashSet<Profile>();
            ProjectContents = new HashSet<ProjectContent>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public virtual ICollection<Package> Packages { get; set; }
        
        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual User User { get; set; }
        
        public virtual ICollection<ProjectContent> ProjectContents { get; set; }
    }
}
