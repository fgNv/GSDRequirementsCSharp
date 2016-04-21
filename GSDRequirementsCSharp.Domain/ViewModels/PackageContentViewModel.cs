using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class PackageContentViewModel
    {
        public Guid Id { get; set; }
        public string Locale { get; set; }
        public string Description { get; set; }

        public bool IsUpdated { get; set; }

        internal static PackageContentViewModel FromModel(PackageContent model)
        {
            return new PackageContentViewModel
            {
                Description = model.Description,
                Locale = model.Locale,
                Id = model.Id,
                IsUpdated = model.IsUpdated
            };
        }
    }
}
