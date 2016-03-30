namespace GSDRequirementsCSharp.Persistence
{
    using Domain;
    using Infrastructure.Persistence;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Package : IEntity<PackageKey>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public Guid CreatorId { get; set; }
        public string Locale { get; set; }
        public virtual Project Project { get; set; }

        PackageKey IEntity<PackageKey>.Id
        {
            get
            {
                return new PackageKey
                {
                    Id = Id,
                    Locale = Locale
                };
            }
        }
    }

    public class PackageKey
    {
        public Guid Id { get; set; }
        public string Locale { get; set; }
    }
}
