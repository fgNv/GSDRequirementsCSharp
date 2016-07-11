using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using System;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    internal class AuditingRepository : GenericRepository<Auditing, Guid>
    {
        public AuditingRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class ActorRepository : GenericRepository<Actor, VersionKey>
    {
        public ActorRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class ActorContentRepository : GenericRepository<ActorContent, LocaleKey>
    {
        public ActorContentRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class ClassDiagramRepository : GenericRepository<ClassDiagram, VersionKey>
    {
        public ClassDiagramRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ClassDiagramContentRepository : GenericRepository<ClassDiagramContent, LocaleKey>
    {
        public ClassDiagramContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ClassRepository : GenericRepository<Class, Guid>
    {
        public ClassRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ClassMethodRepository : GenericRepository<ClassMethod, Guid>
    {
        public ClassMethodRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ClassPropertyRepository : GenericRepository<ClassProperty, Guid>
    {
        public ClassPropertyRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ClassMethodParameterRepository : GenericRepository<ClassMethodParameter, Guid>
    {
        public ClassMethodParameterRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class ClassRelationRepository : GenericRepository<ClassRelationship, Guid>
    {
        public ClassRelationRepository(GSDRequirementsContext context) : base(context) { }
    }

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
    internal class PermissionRepository : GenericRepository<Permission, Guid>
    {
        public PermissionRepository(GSDRequirementsContext context) : base(context) { }
    }
    internal class ProjectRepository : GenericRepository<Project, Guid>
    {
        public ProjectRepository(GSDRequirementsContext context) : base(context) { }
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

    internal class SequenceDiagramRepository : GenericRepository<SequenceDiagram, VersionKey>
    {
        public SequenceDiagramRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class SequenceDiagramContentRepository : GenericRepository<SequenceDiagramContent, LocaleKey>
    {
        public SequenceDiagramContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class SpecificationItemRepository : GenericRepository<SpecificationItem, Guid>
    {
        public SpecificationItemRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UserRepository : GenericRepository<User, int>
    {
        public UserRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseRepository : GenericRepository<UseCase, VersionKey>
    {
        public UseCaseRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseContentRepository : GenericRepository<UseCaseContent, LocaleKey>
    {
        public UseCaseContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseDiagramRepository : GenericRepository<UseCaseDiagram, VersionKey>
    {
        public UseCaseDiagramRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseDiagramCotentRepository : GenericRepository<UseCaseDiagramContent, LocaleKey>
    {
        public UseCaseDiagramCotentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseEntityRelationContentRepository : GenericRepository<UseCaseEntityRelationContent, LocaleKey>
    {
        public UseCaseEntityRelationContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseEntityRelationRepository : GenericRepository<UseCaseEntityRelation, Guid>
    {
        public UseCaseEntityRelationRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCaseEntityRepository : GenericRepository<UseCaseEntity, VersionKey>
    {
        public UseCaseEntityRepository(GSDRequirementsContext context) : base(context) { }
    }


    internal class UseCasePreConditionRepository : GenericRepository<UseCasePreCondition, Guid>
    {
        public UseCasePreConditionRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCasePreConditionContentRepository : GenericRepository<UseCasePreConditionContent, LocaleKey>
    {
        public UseCasePreConditionContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCasePostConditionRepository : GenericRepository<UseCasePostCondition, Guid>
    {
        public UseCasePostConditionRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCasePostConditionContentRepository : GenericRepository<UseCasePostConditionContent, LocaleKey>
    {
        public UseCasePostConditionContentRepository(GSDRequirementsContext context) : base(context) { }
    }

    internal class UseCasesRelationRepository : GenericRepository<UseCasesRelation, Guid>
    {
        public UseCasesRelationRepository(GSDRequirementsContext context) : base(context) { }
    }
}
