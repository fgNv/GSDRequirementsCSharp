using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ActorContentViewModel
    {
        public string Name { get; set; }
        public string Locale { get; set; }

        public static ActorContentViewModel FromModel(ActorContent model)
        {
            return new ActorContentViewModel
            {
                Locale = model.Locale,
                Name = model.Name
            };
        }
    }
}
