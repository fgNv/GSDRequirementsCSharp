using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class CreateClassDiagramCommand : IProjectCommand
    {
        [Required]
        public Guid? PackageId { get; set; }

        public IEnumerable<ClassDiagramContentItem> Contents { get; set; }

        public IEnumerable<ClassItem> Classes { get; set; }

        public IEnumerable<RelationItem> Relations { get; set; }
    }
}
