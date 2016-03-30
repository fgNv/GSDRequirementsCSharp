﻿using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Context
{
    public class ProjectContext : ICurrentProjectContextId
    {
        public const string COOKIE_NAME = "projectContext";

        private static bool CookieDeactivatedCurrentRequest(HttpCookie cookie)
        {
            return cookie.Expires != DateTime.MinValue &&
                cookie.Expires < DateTime.Now;
        }

        public static Guid? Current()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(COOKIE_NAME);
            if (cookie == null || CookieDeactivatedCurrentRequest(cookie))
                return null;
            var cookieValue = cookie.Value;
            return Guid.Parse(cookieValue);
        }

        public Guid Get()
        {
            var projectId = Current();
            if (projectId == null)
                throw new Exception(Sentences.noProjectInContext);
            return projectId.Value;
        }
    }
}