namespace GSDRequirementsCSharp.Domain
{
    using Domain;
    using Infrastructure.Persistence;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ProjectContent : IEntity<LocaleKey>
    {   
        public Guid Id { get; set; }
        
        [StringLength(65535)]
        public string Description { get; set; }
        
        [StringLength(10)]
        public string Locale { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey { Id = Id, Locale = Locale };
            }
        }
    }
}
