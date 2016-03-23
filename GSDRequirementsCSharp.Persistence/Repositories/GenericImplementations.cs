using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>
    {
        public UserRepository(GSDRequirementsContext context): base(context) { }
    }
    public class ContactRepository : GenericRepository<Contact, Guid>
    {
        public ContactRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class ProjectRepository : GenericRepository<Project, Guid>
    {
        public ProjectRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class PackageRepository : GenericRepository<Package, Guid>
    {
        public PackageRepository(GSDRequirementsContext context) : base(context) { }
    }
    public class RequirementRepository : GenericRepository<Requirement, Guid>
    {
        public RequirementRepository(GSDRequirementsContext context) : base(context) { }
    }
}
