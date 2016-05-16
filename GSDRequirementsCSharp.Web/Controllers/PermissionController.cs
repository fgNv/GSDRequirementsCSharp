using GSDRequirementsCSharp.Web.Filters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSDRequirements.Web.Controllers
{
    public class PermissionController : Controller
    {
        [ContextProjectRequired]
        // GET: /<controller>/
        public ActionResult UserManagement()
        {
            return View();
        }
    }
}
