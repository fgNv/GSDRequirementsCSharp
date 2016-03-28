using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Web.Requests.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ICredentialsValidator _credentialsValidator;

        public AuthenticationController(ICredentialsValidator credentialsValidator)
        {
            _credentialsValidator = credentialsValidator;
        }

        public ViewResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public RedirectResult Authenticate(AuthenticateRequest request)
        {
            _credentialsValidator.Validate(request.Login, request.Password);
            FormsAuthentication.SetAuthCookie(request.Login, request.IsPersistentAuthentication);
            var returnUrl = String.IsNullOrWhiteSpace(request.ReturnUrl) ? "/" : request.ReturnUrl;
            return new RedirectResult(returnUrl);
        }

        public RedirectResult Logout()
        {
            var returnUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/";
            FormsAuthentication.SignOut();
            Session.Abandon();
            return new RedirectResult(returnUrl);
        }
    }
}