using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.SpecificationItems
{
    public class RemoveSpecificationItemLinkCommand : ICommand
    {
        [Required]
        public Guid? OriginItemId { get; set; }

        [Required]
        public Guid? TargetItemId { get; set; }
    }
}
