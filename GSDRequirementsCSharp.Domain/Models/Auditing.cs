using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class Auditing : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Project Project { get; set; }
        public Guid ProjectId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public string ActivityDescription { get; set; }

        public DateTime ExecutedAt { get; set; }
    }
}
