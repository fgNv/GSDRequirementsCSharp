using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
{
    public class UseCaseDiagramViewModel : IIssueable
    {
        public Guid Id { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }

        public static UseCaseDiagramViewModel FromModel(UseCaseDiagram model)
        {
            throw new NotImplementedException();
        }
    }
}
