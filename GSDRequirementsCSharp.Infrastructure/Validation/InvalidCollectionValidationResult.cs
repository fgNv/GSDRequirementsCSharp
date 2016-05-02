using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation
{
    public class InvalidCollectionValidationResult : ValidationResult
    {
        public IEnumerable<ValidationResult> Errors { get; private set; }

        public InvalidCollectionValidationResult(IEnumerable<ValidationResult> errors) : base(Sentences.invalidItems)
        {
            Errors = errors;
        }
    }
}
