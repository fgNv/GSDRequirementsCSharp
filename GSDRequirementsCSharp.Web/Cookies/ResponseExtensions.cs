using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Cookies
{
    public static class ResponseExtensions
    {
        public static void RemoveCookie(this HttpResponseBase response, string cookieName)
        {
            var removeCookie = new HttpCookie(cookieName);
            removeCookie.Expires = DateTime.Now.AddDays(-30);
            response.Cookies.Add(removeCookie);
        }
    }
}