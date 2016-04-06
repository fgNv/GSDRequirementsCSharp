using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Queries.Permissions;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.ServiceProviders;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.Filters
{
    public class UserProfileSetterMvcFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var container = DependencyInjection.ContainerExtensions.GetScopedContainer();
            using (container.BeginLifetimeScope())
            {
                var currentUserProvider = container.GetInstance<ICurrentUserRetriever<User>>();
                var currentProjectContextId = container.GetInstance<ICurrentProjectContextId>();
                var permissionByUserAndProjectQueryHandler = container.GetInstance<IQueryHandler<PermissionByUserAndProjectQuery, Permission>>();

                var currentProjectId = currentProjectContextId.Get();
                if (currentProjectId == null || currentProjectId.Value == Guid.Empty)
                    return;

                var currentUser = currentUserProvider.Get();
                if (currentUser == null)
                    return;

                var query = new PermissionByUserAndProjectQuery
                {
                    UserId = currentUser.Id,
                    ProjectId = currentProjectId.Value
                };

                var permission = permissionByUserAndProjectQueryHandler.Handle(query);

                if (permission != null)
                    filterContext.Controller.ViewBag.Profile = permission.Profile;                
            }
        }
    }
}