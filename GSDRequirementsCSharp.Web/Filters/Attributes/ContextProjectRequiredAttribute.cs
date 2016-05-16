using GSDRequirementsCSharp.Web.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Filters.Attributes
{
    public class ContextProjectRequiredAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var projectContext = ProjectContext.Current();
            if (projectContext == null)
                filterContext.Result = new RedirectResult("~/Error/ProjectContextRequired");
            else
                base.OnActionExecuting(filterContext);
        }
    }
}