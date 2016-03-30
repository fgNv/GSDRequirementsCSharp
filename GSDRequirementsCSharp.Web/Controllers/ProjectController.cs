using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSDRequirements.Web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Form()
        {
            return PartialView("~/Views/Project/_Form.cshtml");
        }

        public PartialViewResult Translation()
        {
            return PartialView("~/Views/Project/_Translation.cshtml");
        }
    }
}
