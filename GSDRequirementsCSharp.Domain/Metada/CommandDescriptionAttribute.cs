using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Metadata
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandDescriptionAttribute : Attribute
    {
        private readonly string _description;

        public string Description { get { return _description; } }

        public CommandDescriptionAttribute(string description)
        {
            _description = description;
        }
    }
}
