using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class UseCaseEntityRelationContentViewModel
    {
        public string Description { get; set; }
        public string Locale { get; set; }

        public static UseCaseEntityRelationContentViewModel FromModel(UseCaseEntityRelationContent model)
        {
            return new UseCaseEntityRelationContentViewModel
            {
                Description = model.Description,
                Locale = model.Locale
            };
        }
    }
}
