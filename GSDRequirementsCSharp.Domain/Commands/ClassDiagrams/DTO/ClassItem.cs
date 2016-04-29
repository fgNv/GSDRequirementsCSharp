using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.Commands.ClassDiagrams
{
    public class ClassItem
    {
        public string Name { get; set; }

        public ClassType Type { get; set; }

        public IEnumerable<MethodItem> ClassMethods { get; set; }

        public IEnumerable<PropertyItem> ClassProperties { get; set; }

        public Visibility Visibility { get; set; }

        public Cell Cell { get; set; }
    }
}
