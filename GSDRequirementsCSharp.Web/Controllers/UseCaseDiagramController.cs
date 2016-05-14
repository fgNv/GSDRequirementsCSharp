using GSDRequirementsCSharp.Web.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSDRequirements.Web.Controllers
{
    public class UseCaseDiagramController : Controller
    {
        [ContextProjectRequired]
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        [SkipUserDataSetter]
        public PartialViewResult Management()
        {
            return PartialView("~/Views/UseCaseDiagram/_Management.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult Versions()
        {
            return PartialView("_Versions");
        }

        [SkipUserDataSetter]
        public PartialViewResult Display()
        {
            return PartialView("~/Views/UseCaseDiagram/_Display.cshtml");
        }
    }
}
