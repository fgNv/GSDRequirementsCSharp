using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.CQS.Interfaces
{
    public interface IDeleteCommand<TKey> : ICommand
    {
        TKey Id { get; }
    }
}
