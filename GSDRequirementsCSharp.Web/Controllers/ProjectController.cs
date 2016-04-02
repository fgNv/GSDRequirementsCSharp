using GSDRequirementsCSharp.Web.Context;
using GSDRequirementsCSharp.Web.Cookies;
using GSDRequirementsCSharp.Web.Models;
using GSDRequirementsCSharp.Web.Requests.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirements.Web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            Response.RemoveCookie(ProjectContext.COOKIE_NAME);
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

        public PartialViewResult TranslationList()
        {
            var model = new TranslationListViewModel { NgModelEntityReference = "project" };
            return PartialView("~/Views/Project/_TranslationList.cshtml", model);
        }

        public PartialViewResult Context()
        {
            return PartialView("~/Views/Project/_Context.cshtml");
        }

        public RedirectResult SetContext(SetContextProjectRequest request)
        {
            if(request.GetProjectId() == Guid.Empty)
            {
                Response.RemoveCookie(ProjectContext.COOKIE_NAME);
                return Redirect("/");
            }

            var cookie = new HttpCookie(ProjectContext.COOKIE_NAME);
            cookie.Expires = DateTime.Now.AddDays(30);
            cookie.Value = request.ProjectId;
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);
            return Redirect("/");
        }
    }
}
