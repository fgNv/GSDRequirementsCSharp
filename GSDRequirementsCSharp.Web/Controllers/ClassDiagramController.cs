﻿using GSDRequirementsCSharp.Web.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSDRequirements.Web.Controllers
{
    public class ClassDiagramController : Controller
    {
        [ContextProjectRequired]
        public ActionResult Index()
        {
            return View();
        }

        [SkipUserDataSetter]
        public PartialViewResult Management()
        {
            return PartialView("~/Views/ClassDiagram/_Management.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult Display()
        {
            return PartialView("~/Views/ClassDiagram/_Display.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult Versions()
        {
            return PartialView("~/Views/ClassDiagram/_Versions.cshtml");
        }
    }
}
