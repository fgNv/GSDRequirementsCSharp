using GSDRequirementsCSharp.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ApiExceptionHandling.Handle(context.Exception as dynamic, context);
        }
    }
}