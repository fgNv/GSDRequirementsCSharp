using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    [CommandDescription(nameof(Sentences.issueEdited))]
    public class AddIssueTranslationCommand : UpdateIssueCommand
    {
    }
}
