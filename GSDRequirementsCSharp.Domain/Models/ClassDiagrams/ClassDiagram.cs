namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClassDiagram : IEntity<VersionKey>
    {
        public const string PREFIX = "CD";

        public Guid Id { get; set; }

        public int Version { get; set; }

        public int Identifier { get; set; }

        public bool IsLastVersion { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }
        
        public ICollection<Class> Classes { get; set; }

        public ICollection<ClassRelationship> Relationships { get; set; }

        public virtual ICollection<ClassDiagramContent> Contents
        {
            get; set;
        }

        public virtual SpecificationItem SpecificationItem { get; set; }
        
        VersionKey IEntity<VersionKey>.Id
        {
            get
            {
                return new VersionKey { Id = Id, Version = Version };
            }
        }

        public ClassDiagram()
        {
            Classes = new HashSet<Class>();
            Relationships = new HashSet<ClassRelationship>();
            Contents = new HashSet<ClassDiagramContent>();
        }
    }
}
