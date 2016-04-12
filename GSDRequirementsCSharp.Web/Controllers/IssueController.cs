using GSDRequirementsCSharp.Web.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Controllers
{
    public class IssueController : Controller
    {
        [SkipUserDataSetter]
        public PartialViewResult Create()
        {
            return PartialView("~/Views/Issue/_Create.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult Form()
        {
            return PartialView("~/Views/Issue/_Form.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult ItemIssues()
        {
            return PartialView("~/Views/Issue/_ItemIssues.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult List()
        {
            return PartialView("~/Views/Issue/_ListModalContainer.cshtml");
        }
    }
}