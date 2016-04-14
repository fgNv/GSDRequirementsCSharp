using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class SpecificationItemViewModel
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public PackageViewModel Package { get; set; }
        
        public static SpecificationItemViewModel FromModel(SpecificationItem item)
        {
            return new SpecificationItemViewModel
            {
                Id = item.Id,
                Package = PackageViewModel.FromModel(item.Package),
                Label = item.Label
            };
        }
    }
}
