using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Exceptions
{
    public class ExceptionResponse
    {
        public IEnumerable<string> Messages { get; set; }
    }
}