using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Web.Context;
using GSDRequirementsCSharp.Web.Cookies;
using GSDRequirementsCSharp.Web.Filters.Attributes;
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
        private readonly IQueryHandler<Guid, ProjectViewModel> _projectByIdQueryHandler;
        private readonly ICurrentProjectContextId _currentProjectContextId;

        public ProjectController(IQueryHandler<Guid, ProjectViewModel> projectByIdQueryHandler,
                                 ICurrentProjectContextId currentProjectContextId)
        {
            _projectByIdQueryHandler = projectByIdQueryHandler;
            _currentProjectContextId = currentProjectContextId;
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            Response.RemoveCookie(ProjectContext.COOKIE_NAME);
            return View();
        }
        
        private ViewResult ProjectDetailsView(Guid projectId)
        {
            var projectData = _projectByIdQueryHandler.Handle(projectId);
            if (projectData == null)
                throw new HttpException(404, Sentences.projectNotFound);

            return View("Details", projectData);
        }
        
        public ViewResult CurrentProjectDetails()
        {
            var id = _currentProjectContextId.Get();
            if (id == null)
                throw new HttpException(400, Sentences.noProjectInContext);

            return ProjectDetailsView(id.Value);
        }
        
        public ViewResult Details(Guid id)
        {
            return ProjectDetailsView(id);
        }

        [SkipUserDataSetter]
        public PartialViewResult Form()
        {
            return PartialView("~/Views/Project/_Form.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult Translation()
        {
            return PartialView("~/Views/Project/_Translation.cshtml");
        }

        [SkipUserDataSetter]
        public PartialViewResult TranslationList()
        {
            var model = new TranslationListViewModel { NgModelEntityReference = "project" };
            return PartialView("~/Views/Project/_TranslationList.cshtml", model);
        }

        [SkipUserDataSetter]
        public PartialViewResult Context()
        {
            return PartialView("~/Views/Project/_Context.cshtml");
        }

        public RedirectResult SetContext(SetContextProjectRequest request)
        {
            if (request.GetProjectId() == Guid.Empty)
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
