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
            var removalLabelCookie = new HttpCookie("removing"+cookieName);
            removalLabelCookie.Expires = DateTime.Now.AddDays(-30);
            removeCookie.Expires = DateTime.Now.AddDays(-30);
            response.Cookies.Add(removeCookie);
            response.Cookies.Add(removalLabelCookie);
        }
    }
}