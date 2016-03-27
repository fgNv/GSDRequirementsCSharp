using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Requests.Internationalization
{
    public class SetCurrentCultureRequest
    {
        public string Culture { get; set; }
        public string ReturnUrl { get; set; }
    }
}