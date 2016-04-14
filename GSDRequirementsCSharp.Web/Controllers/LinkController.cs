using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class LinkController : Controller
    {
        public PartialViewResult Management()
        {
            return PartialView("~/Views/Link/_Management.cshtml");
        }
    }
}