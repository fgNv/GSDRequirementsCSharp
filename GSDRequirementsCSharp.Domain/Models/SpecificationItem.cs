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

        /**
         <summary>
            References requirements when the specification item is of type Requirement.
            Targets mulitple requirements to address multiple versions of the artifacts.
         </summary>
         */
        public virtual ICollection<Requirement> Requirements { get; set; }

        /**
         <summary>
            References use cases when the specification item is of type Use Case.
            Targets mulitple use cases to address multiple versions of the artifacts.
         </summary>
         */
        public virtual ICollection<UserCase> UserCases { get; set; }

        /**
         <summary>
            References class diagrams when the specification item is of type Class Diagram.
            Targets mulitple class diagrams to address multiple versions of the artifacts.
         </summary>
         */
        public virtual ICollection<ClassDiagram> ClassDiagrams { get; set; }

        public virtual ICollection<SpecificationItem> LinkedItems { get; set; }
    }
}
