using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Infrastructure.Authentication;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.Projects.CurrentUserProjects
{
    public class CurrentUserProjectsQuery { }

    public class CurrentUserProjectsQueryResult
    {
        public IEnumerable<ProjectOption> Projects { get; set; }
    }

    public class ProjectOption
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ProjectOption(Project project)
        {
            this.Id = project.Id;
            this.Name = project.Name;
        }

        public ProjectOption()
        {

        }
    }

    public class CurrentUserProjectsQueryHandler : IQueryHandler<CurrentUserProjectsQuery, CurrentUserProjectsQueryResult>
    {
        private readonly GSDRequirementsContext _context;
        private readonly ICurrentUserRetriever<User> _currentUserRetriever;

        public CurrentUserProjectsQueryHandler(GSDRequirementsContext context,
                                               ICurrentUserRetriever<User> currentUserRetriever)
        {
            _context = context;
            _currentUserRetriever = currentUserRetriever;
        }

        public CurrentUserProjectsQueryResult Handle(CurrentUserProjectsQuery query)
        {
            var currentUser = _currentUserRetriever.Get();
            var projects = _context.Projects.Where(p => p.OwnerId == currentUser.Id)
                             .OrderBy(p => p.Name)
                             .Select(p => new ProjectOption
                             {
                                 Name = p.Name,
                                 Id = p.Id
                             })
                             .ToList();
            var result = new CurrentUserProjectsQueryResult();
            result.Projects = projects;
            return result;
        }
    }
}
