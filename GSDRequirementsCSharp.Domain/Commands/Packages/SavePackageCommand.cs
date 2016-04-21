using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.Packages
{
    public class SavePackageCommand : IProjectCommand
    {
        [ValidateCollection]        
        public IEnumerable<PackageContentItem> Items { get; set; }        
    }
}
