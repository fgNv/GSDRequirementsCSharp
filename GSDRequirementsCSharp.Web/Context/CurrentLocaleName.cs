using GSDRequirementsCSharp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace GSDRequirementsCSharp.Web.Context
{
    public class CurrentLocaleName : ICurrentLocaleName
    {
        public string Get()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}