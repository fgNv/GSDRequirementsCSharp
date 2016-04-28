using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class ClassItem
    {
        public string Name { get; set; }

        public ClassType Type { get; set; }

        public IEnumerable<MethodItem> Methods { get; set; }

        public IEnumerable<PropertyItem> Proprerties { get; set; }
    }
}
