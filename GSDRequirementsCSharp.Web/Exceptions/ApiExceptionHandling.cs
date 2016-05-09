using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Exceptions;
using GSDRequirementsCSharp.Infrastructure.Validation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
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
            formatter.SerializerSettings
                     .ContractResolver = new CamelCasePropertyNamesContractResolver();
            var content = new ObjectContent<ExceptionResponse>(exceptionMessage, formatter, "application/json");
            return new HttpResponseMessage { Content = content };
        }

        public static void Handle(this DbEntityValidationException e, HttpActionExecutedContext context)
        {
            var errors = e.EntityValidationErrors;
            var messages = errors.Where(i => !i.IsValid)
                                 .SelectMany(i => i.ValidationErrors.Select(err => err.ErrorMessage));

            var response = BuildContent(messages);
            response.StatusCode = HttpStatusCode.InternalServerError;
            context.Response = response;
        }

        public static void Handle(this Exception e, HttpActionExecutedContext context)
        {
            var messages = e.GetMessages();
            var response = BuildContent(messages);
            response.StatusCode = HttpStatusCode.InternalServerError;
            context.Response = response;
        }

        public static void Handle(this AuthenticationFailedException e, HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Forbidden;
            context.Response = response;
        }

        public static void Handle(this NotificationException e, HttpActionExecutedContext context)
        {
            var response = BuildContent(e.Messages);
            response.StatusCode = HttpStatusCode.BadRequest;
            context.Response = response;
        }
        
        public static void Handle(this CommandValidationException e, HttpActionExecutedContext context)
        {
            var response = BuildContent(e.Messages);
            response.StatusCode = HttpStatusCode.BadRequest;            
            context.Response = response;
        }

        public static void Handle(this PermissionException e, HttpActionExecutedContext context)
        {
            var response = BuildContent(new[] { e.Message });
            response.StatusCode = HttpStatusCode.Forbidden;
            context.Response = response;
        }
    }
}