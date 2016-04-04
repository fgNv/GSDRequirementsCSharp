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

        public PartialViewResult Form()
        {
            return PartialView("Form");
        }

        public PartialViewResult Translation()
        {
            return PartialView("Translation");
        }

        public PartialViewResult TranslationList()
        {
            var model = new TranslationListViewModel { NgModelEntityReference = "requirement" };
            return PartialView("_TranslationList", model);
        }
    }
}
