using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class UseCaseController : Controller
    {
        public PartialViewResult Display()
        {
            return PartialView("~/Views/UseCase/_Display.cshtml");
        }
    }
}