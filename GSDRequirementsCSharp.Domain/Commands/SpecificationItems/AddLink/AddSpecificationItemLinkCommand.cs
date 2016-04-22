﻿using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.SpecificationItems
{
    public class AddSpecificationItemLinkCommand : IProjectCommand
    {
        /** <summary>Origin item id</summary>  */
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public Guid? TargetItemId { get; set; }

        public bool IsBidirectional { get; set; }

        public AddSpecificationItemLinkCommand()
        {
            IsBidirectional = true;
        }
    }
}