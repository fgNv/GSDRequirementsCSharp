using GSDRequirementsCSharp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace GSDRequirements.Web.Controllers
{
    public class PackageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Form()
        {
            return PartialView("~/Views/Package/_Form.cshtml");
        }

        public PartialViewResult Translation()
        {
            return PartialView("~/Views/Package/_Translation.cshtml");
        }

        public PartialViewResult TranslationList()
        {
            var model = new TranslationListViewModel { NgModelEntityReference = "package" };
            return PartialView("~/Views/Package/_TranslationList.cshtml", model);
        }
    }
}
