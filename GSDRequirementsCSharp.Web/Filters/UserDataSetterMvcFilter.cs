using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain.ViewModels;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Persistence.Queries.Packages.CurrentProject;
using GSDRequirementsCSharp.Web.Filters.Attributes;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class UserDataSetterMvcFilter : ActionFilterAttribute, IActionFilter
    {
        private void SetProjectPermissionData(User currentUser, ActionExecutingContext filterContext, Container container)
        {
            var currentProjectContextId = container.GetInstance<ICurrentProjectContextId>();
            var permissionByUserAndProjectQueryHandler = container.GetInstance<IQueryHandler<PermissionByUserAndProjectQuery, Permission>>();

            var currentProjectId = currentProjectContextId.Get();

            if (currentProjectId == null || currentProjectId.Value == Guid.Empty)
                return;

            var query = new PermissionByUserAndProjectQuery
            {
                UserId = currentUser.Id,
                ProjectId = currentProjectId.Value
            };

            var permission = permissionByUserAndProjectQueryHandler.Handle(query);

            if (permission == null)
                return;

            filterContext.Controller.ViewBag.Profile = permission.Profile;

            var packagesQueryHandler = container.GetInstance<IQueryHandler<PackagesCurrentProjectQuery, IEnumerable<PackageViewModel>>>();
            var packages = packagesQueryHandler.Handle(null);
            filterContext.Controller.ViewBag.AnyPackagesRegistered = packages.Count() > 0;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var attributes = filterContext.ActionDescriptor
                                          .GetCustomAttributes(false);

            if (attributes.Any(a => a is SkipUserDataSetterAttribute))
                return;

            var container = DependencyInjection.ContainerExtensions.GetScopedContainer();
            using (container.BeginLifetimeScope())
            {
                var currentUserProvider = container.GetInstance<ICurrentUserRetriever<User>>();

                var currentUser = currentUserProvider.Get();
                if (currentUser == null)
                    return;

                filterContext.Controller.ViewBag.UserContact = currentUser.Contact;
                SetProjectPermissionData(currentUser, filterContext, container);
            }
        }
    }
}