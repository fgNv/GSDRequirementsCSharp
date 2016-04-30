using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation
{
    public class DataAnnotationValidator : IValidator
    {
        public IEnumerable<string> Validate(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var items = Validator.TryValidateObject(
                model, context, results, true
            );

            var invalidCollectionErrors = results.Where(item => item is InvalidCollectionValidationResult)
                                                 .Select(item => item as InvalidCollectionValidationResult)
                                                 .SelectMany(item => item.Errors.Select(e => e.ErrorMessage));

            var invalidItemErrors = results.Where(item => !(item is InvalidCollectionValidationResult))
                                                 .Select(item => item.ErrorMessage);

            return invalidItemErrors.Union(invalidCollectionErrors);
        }
    }
}
