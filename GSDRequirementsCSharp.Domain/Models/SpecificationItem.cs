namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public virtual ICollection<Issue> Issues { get; set; }

        public virtual ICollection<Requirement> Requirements { get; set; }

        public virtual ICollection<UserCase> UserCases { get; set; }

        public virtual ICollection<ClassDiagram> ClassDiagrams { get; set; }        
    }
}
