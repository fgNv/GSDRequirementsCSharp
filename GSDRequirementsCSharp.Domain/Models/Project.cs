namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Linq;

    public class Project : IEntity<Guid>
    {
        public Project()
        {
            Packages = new HashSet<Package>();
            Profiles = new HashSet<Profile>();
            ProjectContents = new HashSet<ProjectContent>();
        }

        public Guid Id { get; set; }
        
        public int OwnerId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
        
        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual User Owner { get; set; }
        
        public virtual ICollection<ProjectContent> ProjectContents { get; set; }

        public bool Active { get; set; }

        public int Identifier { get; set; }

        public string GetName()
        {
            var currentLocaleName = Thread.CurrentThread.CurrentCulture.Name;
            var currentLocaleContent = ProjectContents.FirstOrDefault(pc => pc.Locale == currentLocaleName);
            if (currentLocaleContent != null)
                return currentLocaleContent.Name;

            var enUsContent = ProjectContents.FirstOrDefault(pc => pc.Locale == "en-US");
            if (enUsContent != null)
                return enUsContent.Name;

            return ProjectContents.FirstOrDefault()?.Name ?? "-";
        }
    }
}
