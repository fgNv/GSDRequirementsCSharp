using GSDRequirementsCSharp.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class HandleMvcException : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            MvcExceptionHandling.Handle(filterContext.Exception as dynamic, filterContext);
        }
    }
}