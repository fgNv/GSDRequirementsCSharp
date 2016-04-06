using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public interface IProjectOwnerCommand : ICommand
    {
        Guid ProjectId { get; }
    }
}
