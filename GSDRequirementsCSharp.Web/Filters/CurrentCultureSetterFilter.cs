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
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class CurrentCultureSetterFilter : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var localeCookie = filterContext.HttpContext
                                            .Request
                                            .Cookies
                                            .Get(Internationalization.LOCALE_COOKIE_NAME);

            var locale = localeCookie?.Value ?? "en-US";
            var cultureInfo = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            base.OnActionExecuting(filterContext);
        }
    }
}