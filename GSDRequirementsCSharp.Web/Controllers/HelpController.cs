using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    [AllowAnonymous]
    public class HelpController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}