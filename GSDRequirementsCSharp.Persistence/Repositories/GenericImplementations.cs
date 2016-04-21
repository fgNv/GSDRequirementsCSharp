﻿using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    internal class ContactRepository : GenericRepository<Contact, Guid>
    {
        public ContactRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class IssueRepository : GenericRepository<Issue, Guid>
    {
        public IssueRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class IssueContentRepository : GenericLocaleRepository<IssueContent>
    {
        public IssueContentRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class IssueCommentRepository : GenericRepository<IssueComment, Guid>
    {
        public IssueCommentRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class IssueCommentContentRepository : GenericLocaleRepository<IssueCommentContent>
    {
        public IssueCommentContentRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class ProjectRepository : GenericRepository<Project, Guid>
    {
        public ProjectRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class PermissionRepository : GenericRepository<Permission, Guid>
    {
        public PermissionRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ProjectContentRepository : GenericLocaleRepository<ProjectContent>
    {
        public ProjectContentRepository(GSDRequirementsContext context) : base(context) { }
    }
    
    internal class PackageRepository : GenericRepository<Package, Guid>
    {
        public PackageRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class PackageContentRepository : GenericLocaleRepository<PackageContent>
    {
        public PackageContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class RequirementRepository : VersionKeyRepository<Requirement>
    {
        public RequirementRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class RequirementContentRepository : GenericLocaleRepository<RequirementContent>
    {
        public RequirementContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class SpecificationItemRepository : GenericRepository<SpecificationItem, Guid>
    {
        public SpecificationItemRepository(GSDRequirementsContext context) : base(context) { }
    }
    
    internal class UserRepository : GenericRepository<User, int>
    {
        public UserRepository(GSDRequirementsContext context) : base(context) { }
    }
}
