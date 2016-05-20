using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class PostConditionContentViewModel
    {
        public string Locale { get; set; }
        public string Description { get; set; }

        public static PostConditionContentViewModel FromModel(UseCasePostConditionContent model)
        {
            return new PostConditionContentViewModel
            {
                Description = model.Description,
                Locale = model.Locale
            };
        }
    }
}
