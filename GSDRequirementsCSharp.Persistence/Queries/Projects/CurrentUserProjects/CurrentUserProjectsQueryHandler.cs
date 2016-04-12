using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GSDRequirementsCSharp.Infrastructure.Context;

namespace GSDRequirementsCSharp.Persistence.Queries
{
    public class CurrentUserProjectsQuery { }

    public class CurrentUserProjectsQueryResult
    {
        public IEnumerable<ProjectOption> Projects { get; set; }
    }

    internal class CurrentUserProjectsQueryHandler : IQueryHandler<CurrentUserProjectsQuery, CurrentUserProjectsQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;
        private readonly ICurrentLocaleName _currentLocaleName;

        public CurrentUserProjectsQueryHandler(GSDRequirementsContext context,
                                               ICurrentUserRetriever<User> currentUserRetriever,
                                               ICurrentLocaleName currentLocaleName)
        {
            _context = context;
            _currentUserRetriever = currentUserRetriever;
            _currentLocaleName = currentLocaleName;
        }

        public CurrentUserProjectsQueryResult Handle(CurrentUserProjectsQuery query)
        {
            var currentUser = _currentUserRetriever.Get();
            if (currentUser == null)
                throw new Exception(Sentences.youMustBeLoggedIn);

            var projects = _context.Projects
                                   .Include(p => p.ProjectContents)
                                   .Where(p => p.Permissions.Any(perm => perm.UserId == currentUser.Id) &&
                                               p.Active)
                                   .ToList()
                                   .Select(ProjectOption.FromModel)
                                   .OrderBy(p => p.Name)
                                   .ToList();

            var result = new CurrentUserProjectsQueryResult();
            result.Projects = projects;
            return result;
        }
    }
}
