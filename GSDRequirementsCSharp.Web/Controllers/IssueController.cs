using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class IssueController : Controller
    {
        public PartialViewResult Create()
        {
            return PartialView("~/Views/Issue/_Create.cshtml");
        }

        public PartialViewResult Form()
        {
            return PartialView("~/Views/Issue/_Form.cshtml");
        }
    }
}