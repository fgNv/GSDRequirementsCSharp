using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace GSDRequirementsCSharp.Web.Api.Globals
{
    public class CookieRemoval
    {
        public static CookieHeaderValue Get(HttpRequestMessage request, string cookieName)
        {
            var currentCookie = request.Headers.GetCookies(cookieName).FirstOrDefault();
            if (currentCookie == null)
                return null;
            
            return new CookieHeaderValue(cookieName, "")
            {
                Expires = DateTimeOffset.Now.AddDays(-1),
                Domain = currentCookie.Domain,
                Path = currentCookie.Path
            };

        }
    }
}