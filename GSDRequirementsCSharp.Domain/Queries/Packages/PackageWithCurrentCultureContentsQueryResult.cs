using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.Packages
{
    public class PackageWithCurrentCultureContentsQueryResult
    {
        public Package Package { get; set; }
        public PackageContent PackageContent { get; set; }
    }
}
