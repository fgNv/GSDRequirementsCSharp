using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class SequenceDiagram : IEntity<VersionKey>
    {
        public const string PREFIX = "SD";

        public Guid Id { get; set; }

        public int Version { get; set; }

        public int Identifier { get; set; }

        public ICollection<SequenceDiagramContent> Contents { get; set; }

        public bool IsLastVersion { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }

        public virtual SpecificationItem SpecificationItem { get; set; }

        VersionKey IEntity<VersionKey>.Id
        {
            get
            {
                return new VersionKey { Id = Id, Version = Version };
            }
        }

        public SequenceDiagram()
        {
            Contents = new HashSet<SequenceDiagramContent>();
        }
    }
}
