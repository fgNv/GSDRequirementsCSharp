using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace GSDRequirementsCSharp.Web.Exceptions
{
    public static class ApiExceptionHandling
    {
        private static HttpResponseMessage BuildContent(IEnumerable<string> messages)
        {
            var exceptionMessage = new ExceptionResponse { Messages = messages };
            var formatter = new JsonMediaTypeFormatter();
            var content = new ObjectContent<ExceptionResponse>(exceptionMessage, formatter, "application/json");
            return new HttpResponseMessage { Content = content };
        }

        public static void Handle(this Exception e, HttpActionExecutedContext context)
        {
            var response = BuildContent(new[] { e.Message });
            context.Response = response;
        }

        public static void Handle(this AuthenticationFailedException e, HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage();
            context.Response = response;
        }

        public static void Handle(this NotificationException e, HttpActionExecutedContext context)
        {
            var response = BuildContent(e.Messages);
            context.Response = response;
        }
    }
}