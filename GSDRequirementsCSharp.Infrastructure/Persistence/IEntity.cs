using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Persistence
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
