using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class UseCaseContent : IEntity<LocaleKey>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Locale { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(65535)]
        public string Description { get; set; }

        public UseCase UseCase { get; set; }
        public Guid UseCaseId { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey { Id = Id, Locale = Locale };
            }
        }
    }
}
