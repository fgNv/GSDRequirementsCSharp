using GSDRequirementsCSharp.Web.Models;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class CurrentCultureSetterMvcFilter : ActionFilterAttribute, IActionFilter
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