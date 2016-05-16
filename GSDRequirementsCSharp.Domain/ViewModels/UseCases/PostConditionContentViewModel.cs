using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
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
