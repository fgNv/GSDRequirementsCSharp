using GSDRequirementsCSharp.Web.Filters.Attributes;
using GSDRequirementsCSharp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSDRequirements.Web.Controllers
{
    public class RequirementController : Controller
    { 
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        [SkipUserDataSetter]
        public PartialViewResult Form()
        {
            return PartialView("Form");
        }

        [SkipUserDataSetter]
        public PartialViewResult Translation()
        {
            return PartialView("Translation");
        }

        [SkipUserDataSetter]
        public PartialViewResult TranslationList()
        {
            var model = new TranslationListViewModel { NgModelEntityReference = "requirement" };
            return PartialView("_TranslationList", model);
        }
    }
}
