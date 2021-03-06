﻿using GSDRequirementsCSharp.Domain.Metadata;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Infrastructure.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    [CommandDescription(nameof(Sentences.classDiagramCreated))]
    public class CreateClassDiagramCommand : IProjectCommand
    {
        [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = nameof(ValidationMessages.packageIsARequiredField))]
        public Guid? PackageId { get; set; }

        [ValidateCollection]
        [AtLeastOneElement(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.classDiagramNameIsARequiredField))]
        public IEnumerable<ClassDiagramContentItem> Contents { get; set; }

        [ValidateCollection]
        public IEnumerable<ClassItem> Classes { get; set; }

        [ValidateCollection]
        public IEnumerable<RelationItem> Relations { get; set; }
    }
}
