using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
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
