using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
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
