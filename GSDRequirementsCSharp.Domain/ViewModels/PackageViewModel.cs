using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class PackageViewModel
    {
        public Guid Id { get; set; }
        public int Identifier { get; set; }
        public IEnumerable<PackageContentViewModel> Contents { get; set; }

        public static PackageViewModel FromModel(Package package)
        {
            return new PackageViewModel
            {
                Contents = package.Contents?.Select(PackageContentViewModel.FromModel),
                Id = package.Id,
                Identifier = package.Identifier
            };
        }
    }
}
