using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation
{
    public interface IValidator
    {
        IEnumerable<string> Validate(object model);
    }
}
