using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Internationalization
{
    public interface ITranslatable
    {
        string Locale { get; }
    }
}
