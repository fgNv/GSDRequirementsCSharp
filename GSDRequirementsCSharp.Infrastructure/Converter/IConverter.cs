using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Converter
{
    public interface IConverter<TFrom,TTo>
    {
        TTo Convert(TFrom input);
    }
}
