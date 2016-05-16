using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Domain.Validation;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    [CommandDescription(nameof(Sentences.packageEdited))]
    public class AddPackageTranslationCommand : UpdatePackageCommand
    {
        
    }
}
