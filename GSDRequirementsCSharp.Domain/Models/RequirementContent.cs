namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class RequirementContent : IEntity<LocaleKey>
    {
        public Guid Id { get; set; }
        
        [StringLength(10)]
        public string Locale { get; set; }

        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(150)]
        public string Condition { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        public Guid? CreatorId { get; set; }

        public virtual Requirement Requirement { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey { Id = Id, Locale = Locale };
            }
        }
    }
}
