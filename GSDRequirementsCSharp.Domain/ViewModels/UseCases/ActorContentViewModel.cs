using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
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
