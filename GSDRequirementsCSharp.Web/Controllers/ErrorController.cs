using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult ProjectContextRequired()
        {
            Response.StatusCode = 400;
            return View();
        }
    }
}