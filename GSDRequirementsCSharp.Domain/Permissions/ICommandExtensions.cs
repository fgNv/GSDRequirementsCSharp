using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using GSDRequirementsCSharp.Infrastructure.ServiceProviders;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSDRequirementsCSharp.Domain.Queries.Permissions;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.Context;
using GSDRequirementsCSharp.Infrastructure.Exceptions;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using GSDRequirementsCSharp.Domain.Models;

namespace GSDRequirementsCSharp.Domain.Permissions
{
    public static class ICommandExtensions
    {
        public static void VerifyPermission(this ICommand command, IServiceProvider serviceProvider)
        {
            return;
        }

        private static User GetCurrentUser(IServiceProvider serviceProvider)
        {
            var currentUserRetriever = serviceProvider.GetService<ICurrentUserRetriever<User>>();
            var currentUser = currentUserRetriever.Get();
            if (currentUser == null)
                throw new PermissionException(Sentences.youMustBeLoggedIn);
            return currentUser;
        }

        private static Permission GetPermissionCurrentUserCurrentProject(IServiceProvider serviceProvider)
        {
            var permissionByUserAndProjectQueryHandler = serviceProvider.GetService<IQueryHandler<PermissionByUserAndProjectQuery, Permission>>();
            var currentProjectContextId = serviceProvider.GetService<ICurrentProjectContextId>();

            var currentProjectId = currentProjectContextId.Get();
            if (currentProjectId == Guid.Empty)
                throw new PermissionException(Sentences.noProjectInContext);
            
            var currentUser = GetCurrentUser(serviceProvider);

            var query = new PermissionByUserAndProjectQuery
            {
                ProjectId = currentProjectId.Value,
                UserId = currentUser.Id
            };
            var permission = permissionByUserAndProjectQueryHandler.Handle(query);
            return permission;
        }

        public static void VerifyPermission(this IProjectCommand command, IServiceProvider serviceProvider)
        {
            var permission = GetPermissionCurrentUserCurrentProject(serviceProvider);
            if (permission == null)
                throw new PermissionException(Sentences.youDontHavePermissionToAccessThisProject);

            if (permission.Profile != Profile.Editor && permission.Profile != Profile.ProjectOwner)
                throw new PermissionException(Sentences.youMustHaveEditorPermissionForThisProjectToExecuteThisAction);
        }

        public static void VerifyPermission(this IProjectOwnerCommand command, IServiceProvider serviceProvider)
        {
            var projectRepository = serviceProvider.GetService<IRepository<Project, Guid>>();
            var currentUser = GetCurrentUser(serviceProvider);

            var project = projectRepository.Get(command.ProjectId);

            if (project == null)
                throw new Exception(Sentences.projectNotFound);

            if (project.OwnerId != currentUser.Id)
                throw new PermissionException(Sentences.youMustBeTheProjectOwnerToExecuteThisAction);
        }
    }
}
