using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class VersionItem
    {
        public string Label { get; set; }
        public int Version { get; set; }
        public Guid Id { get; set; }
    }
}
