using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ItemLinkViewModel
    {
        public SpecificationItemViewModel Origin { get; set; }
        public SpecificationItemViewModel Target { get; set; }

        public static ItemLinkViewModel FromModel(SpecificationItem origin, SpecificationItem target)
        {
            return new ItemLinkViewModel
            {
                Origin = SpecificationItemViewModel.FromModel(origin),
                Target = SpecificationItemViewModel.FromModel(target)
            };
        }
    }
}
