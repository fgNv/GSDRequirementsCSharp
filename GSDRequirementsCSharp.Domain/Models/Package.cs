namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Package : IEntity<Guid>
    {
        public const string PREFIX = "PKG";

        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public ICollection<PackageContent> Contents { get; set; }
        public int CreatorId { get; set; }        
        public virtual Project Project { get; set; }
        public bool Active { get; set; }
        public int Identifier { get; set; }
    }
}
