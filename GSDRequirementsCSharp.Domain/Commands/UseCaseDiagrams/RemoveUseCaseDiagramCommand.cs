using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.UseCaseDiagrams
{
    [CommandDescription(nameof(Sentences.useCaseDiagramRemoved))]
    public class RemoveUseCaseDiagramCommand : IProjectCommand
    {
        public Guid Id { get; set; }

        public static implicit operator RemoveUseCaseDiagramCommand(Guid id)
        {
            return new RemoveUseCaseDiagramCommand { Id = id };
        }
    }
}
