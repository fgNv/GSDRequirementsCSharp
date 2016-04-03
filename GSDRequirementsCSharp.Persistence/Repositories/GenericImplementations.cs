using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    public class GenericUserRepository : GenericRepository<User, int>
    {
        public GenericUserRepository(GSDRequirementsContext context): base(context) { }
    }
    public class ContactRepository : GenericRepository<Contact, Guid>
    {
        public ContactRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class ProjectRepository : GenericRepository<Project, Guid>
    {
        public ProjectRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class RequirementRepository : VersionKeyRepository<Requirement>
    {
        public RequirementRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class ProjectContentRepository : GenericLocaleRepository<ProjectContent>
    {
        public ProjectContentRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class SpecificationItemRepository : GenericRepository<SpecificationItem, Guid>
    {
        public SpecificationItemRepository(GSDRequirementsContext context) : base(context) { }
    }

    public class PackageRepository : GenericRepository<Package, Guid>
    {
        public PackageRepository(GSDRequirementsContext context) : base(context) { }
    }

    public class PackageContentRepository : GenericLocaleRepository<PackageContent>
    {
        public PackageContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    public class RequirementContentRepository : GenericLocaleRepository<RequirementContent>
    {
        public RequirementContentRepository(GSDRequirementsContext context) : base(context) { }
    }
}
