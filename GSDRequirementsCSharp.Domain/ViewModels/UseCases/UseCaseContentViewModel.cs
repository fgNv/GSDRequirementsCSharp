using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class UseCaseContentViewModel
    {
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public static UseCaseContentViewModel FromModel(UseCaseContent model)
        {
            return new UseCaseContentViewModel
            {
                Locale = model.Locale,
                Name = model.Name,
                Description = model.Description,
                Path = model.Path
            };
        }
    }
}
