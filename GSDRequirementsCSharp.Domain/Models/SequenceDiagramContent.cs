using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class SequenceDiagramContent : IEntity<LocaleKey>
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }

        public int Version { get; set; }

        public int? CreatorId { get; set; }

        public User Creator { get; set; }

        [StringLength(10)]
        public string Locale { get; set; }

        public SequenceDiagram SequenceDiagram { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey { Id = Id, Locale = Locale };
            }
        }
    }
}
