using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class PreConditionContentViewModel
    {
        public string Locale { get; set; }
        public string Description { get; set; }

        public static PreConditionContentViewModel FromModel(UseCasePreConditionContent model)
        {
            return new PreConditionContentViewModel
            {
                Description = model.Description,
                Locale = model.Locale
            };
        }
    }
}
