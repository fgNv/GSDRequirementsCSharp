using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence
{
    public partial class Project : IEntity<Guid>
    {
        public Guid Id
        {
            get
            {
                return id;
            }
        }
    }
}
