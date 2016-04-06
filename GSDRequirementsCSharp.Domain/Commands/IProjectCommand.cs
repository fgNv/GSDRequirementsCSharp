using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    /**
     * 
     <summary>
        Command handlers that can be executed 
        only by users with ProjectOwner or Editor permission
     </summary>
     */
    public interface IProjectCommand : ICommand
    {
    }
}
