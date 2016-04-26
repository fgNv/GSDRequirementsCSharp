using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public enum RelationType
    {
        Association = 10,
        Inheritance = 300,
        Composition = 500,
        Aggregation = 700,
        Realization = 900,
    }
}
