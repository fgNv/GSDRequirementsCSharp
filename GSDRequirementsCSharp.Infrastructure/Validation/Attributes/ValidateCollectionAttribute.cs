using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation.Attributes
{
    public class ValidateCollectionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IEnumerable))
                return ValidationResult.Success;

            var enumerable = value as IEnumerable;

            var errorsList = new List<ValidationResult>();

            foreach (var item in enumerable)
            {
                var context = new ValidationContext(item);
                var results = new List<ValidationResult>();
                var items = Validator.TryValidateObject(item, context, results, true);

                errorsList.AddRange(results);
            }
            if (errorsList.Any())
                return new InvalidCollectionValidationResult(errorsList);

            return ValidationResult.Success;
        }
    }
}
