using GSDRequirementsCSharp.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class CurrentCultureSetterApiFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var cookieName = Internationalization.LOCALE_COOKIE_NAME;
            var localeCookie = actionContext.Request
                                            .Headers
                                            .GetCookies()
                                            .SelectMany(c => c.Cookies)
                                            .FirstOrDefault(c => c.Name == cookieName);

            var locale = localeCookie?.Value ?? "en-US";
            var cultureInfo = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            base.OnActionExecuting(actionContext);
            
        }
    }
}