namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;

    public class SpecificationItem : IEntity<Guid>
    {
        public SpecificationItem()
        {
            Issues = new HashSet<Issue>();
        }

        public Guid Id { get; set; }

        public Guid PackageId { get; set; }

        public Package Package { get; set; }
    
        public bool Active { get; set; }

        public string Label { get; set; }

        public SpecificationItemType Type { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
        
        public virtual ICollection<SpecificationItem> LinkedItems { get; set; }
    }
}
