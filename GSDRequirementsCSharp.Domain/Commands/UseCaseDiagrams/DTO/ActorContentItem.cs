﻿using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class ActorContentItem
    {
        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName =nameof(ValidationMessages.nameIsARequiredField))]
        public string Name { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.localeIsARequiredField))]
        public string Locale { get; set; }
    }
}
