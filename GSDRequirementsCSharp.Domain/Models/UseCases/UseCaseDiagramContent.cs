using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class UseCaseDiagramContent : IEntity<LocaleKey>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Locale { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public UseCaseDiagram UseCaseDiagram { get; set; }
        
        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey {  Id = Id, Locale = Locale };
            }
        }
    }
}
