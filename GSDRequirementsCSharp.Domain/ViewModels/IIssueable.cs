using GSDRequirementsCSharp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public interface IIssueable
    {
        Guid Id { get; }
        IEnumerable<IssueViewModel> Issues { set; }
    }
}
