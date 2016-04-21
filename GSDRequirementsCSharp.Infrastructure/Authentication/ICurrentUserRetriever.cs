using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Authentication
{
    public interface ICurrentUserRetriever<T> where T : class
    {
        T Get();
    }
}
