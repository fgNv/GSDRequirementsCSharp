using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Exceptions
{
    public static class MvcExceptionHandling
    {
        public static void Handle(this Exception e, ExceptionContext context)
        {
            //context.Result
        }

        public static void Handle(this AuthenticationFailedException e, ExceptionContext context)
        {
            var viewResult = new ViewResult();
            context.ExceptionHandled = true; 
            viewResult.ViewName = "~/Views/Home/Login.cshtml";
            viewResult.ViewBag.Message = Sentences.authenticationFailed;
            context.Result = viewResult;
        }
    }
}