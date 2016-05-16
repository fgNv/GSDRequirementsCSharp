using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels.UseCases
{
    public class UseCaseDiagramContentViewModel
    {
        public string Name { get; set; }

        public string Locale { get; set; }

        public static UseCaseDiagramContentViewModel FromModel(UseCaseDiagramContent model)
        {
            return new UseCaseDiagramContentViewModel
            {
                Locale = model.Locale,
                Name = model.Name
            };
        }
    }
}
