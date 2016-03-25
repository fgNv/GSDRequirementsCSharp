using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Requests.Authentication
{
    public class AuthenticateRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsPersistentAuthentication { get; set; }
    }
}