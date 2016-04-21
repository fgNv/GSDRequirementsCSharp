using GSDRequirementsCSharp.Web.Models;
using GSDRequirementsCSharp.Web.Requests.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class InternationalizationController : Controller
    {
        // POST: Internationalization
        [HttpPost]
        [AllowAnonymous]
        public RedirectResult SetCurrentCulture(SetCurrentCultureRequest request)
        {
            var cookie = new HttpCookie(Internationalization.LOCALE_COOKIE_NAME);
            cookie.Expires = DateTime.Now.AddDays(30);
            cookie.Value = request.Culture;
            Response.Cookies.Add(cookie);
            return Redirect(request.ReturnUrl);
        }
    }
}