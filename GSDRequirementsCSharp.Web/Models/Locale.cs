using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Models
{
    public class Locale
    {
        public string Name { get; private set; }
        public string Label { get;private  set; }

        public Locale(string name, string label)
        {
            Name = name;
            Label = label;
        }
    }
}