using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class AddSpecificationItemLinkCommand : IProjectCommand
    {
        /** <summary>Origin item id</summary>  */
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.originIsARequiredField))]
        public Guid? Id { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.targetIsARequiredField))]
        public Guid? TargetItemId { get; set; }

        public bool IsBidirectional { get; set; }

        public AddSpecificationItemLinkCommand()
        {
            IsBidirectional = true;
        }
    }
}
