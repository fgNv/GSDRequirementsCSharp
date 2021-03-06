namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Common;
    using Domain;
    using Mappings;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using MySql.Data.Entity;
    using Mappings.UseCases;
    using Domain.Models;
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    internal class GSDRequirementsContext : DbContext
    {
        public GSDRequirementsContext() : base(GetDbConnection(), true)
        {
            Database.SetInitializer<GSDRequirementsContext>(null);
        }
        
        public static DbConnection GetDbConnection()
        {
            var conn = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection();
            var connString = Environment.GetEnvironmentVariable("gsd_requirements_conn_string");
            conn.ConnectionString = connString;
            return conn;
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Auditing> Auditings { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassDiagram> ClassDiagrams { get; set; }
        public virtual DbSet<ClassMethod> ClassMethods { get; set; }
        public virtual DbSet<ClassMethodParameter> ClassMethodParameters { get; set; }
        public virtual DbSet<ClassProperty> ClassProperties { get; set; }
        public virtual DbSet<ClassRelationship> ClassRelationships { get; set; }
        public virtual DbSet<SequenceDiagram> SequenceDiagrams{ get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueComment> IssueComments { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectContent> ProjectContents { get; set; }
        public virtual DbSet<Requirement> Requirements { get; set; }
        public virtual DbSet<RequirementContent> RequirementContents { get; set; }
        public virtual DbSet<RequirementRisk> RequirementRisks { get; set; }
        public virtual DbSet<SpecificationItem> SpecificationItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UseCase> UseCases { get; set; }
        public virtual DbSet<UseCaseDiagram> UseCaseDiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ActorMapping());
            modelBuilder.Configurations.Add(new UseCaseMapping());
            modelBuilder.Configurations.Add(new UseCaseEntityMapping());
            modelBuilder.Configurations.Add(new AuditingMapping());

            modelBuilder.Configurations.Add(new ActorContentMapping());

            modelBuilder.Configurations.Add(new ClassMapping());
            modelBuilder.Configurations.Add(new ClassDiagramMapping());
            modelBuilder.Configurations.Add(new ClassDiagramContentMapping());
            modelBuilder.Configurations.Add(new ClassMethodMapping());
            modelBuilder.Configurations.Add(new ClassMethodParameterMapping());
            modelBuilder.Configurations.Add(new ClassPropertyMapping());
            modelBuilder.Configurations.Add(new ClassRelationshipMapping());
            modelBuilder.Configurations.Add(new ContactMapping());
            modelBuilder.Configurations.Add(new IssueMapping());
            modelBuilder.Configurations.Add(new IssueCommentMapping());
            
            modelBuilder.Configurations.Add(new IssueCommentContentMapping());
            modelBuilder.Configurations.Add(new IssueContentMapping());

            modelBuilder.Configurations.Add(new PackageMapping());
            modelBuilder.Configurations.Add(new PackageContentMapping());
            modelBuilder.Configurations.Add(new PermissionMapping());
            modelBuilder.Configurations.Add(new ProjectMapping());
            modelBuilder.Configurations.Add(new ProjectContentMapping());
            modelBuilder.Configurations.Add(new RequirementMapping());
            modelBuilder.Configurations.Add(new RequirementContentMapping());
            modelBuilder.Configurations.Add(new RequirementRiskMapping());
            modelBuilder.Configurations.Add(new SpecificationItemMapping());

            modelBuilder.Configurations.Add(new UserMapping());

            modelBuilder.Configurations.Add(new UseCaseContentMapping());
            modelBuilder.Configurations.Add(new UseCaseDiagramContentMapping());
            modelBuilder.Configurations.Add(new UseCaseDiagramMapping());
            
            modelBuilder.Configurations.Add(new UseCaseEntityRelationMapping());
            
            modelBuilder.Configurations.Add(new UseCaseEntityRelationContentMapping());
            
            modelBuilder.Configurations.Add(new UseCasePreConditionMapping());
            modelBuilder.Configurations.Add(new UseCasePreConditionContentMapping());
            modelBuilder.Configurations.Add(new UseCasePostConditionMapping());
            modelBuilder.Configurations.Add(new UseCasePostConditionContentMapping());

            modelBuilder.Configurations.Add(new UseCasesRelationMapping());

            modelBuilder.Configurations.Add(new SequenceDiagramMapping());
            modelBuilder.Configurations.Add(new SequenceDiagramContentMapping());
        }
    }
}
