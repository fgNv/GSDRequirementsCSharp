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

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GSDRequirementsContext : DbContext
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
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassContent> ClassContents { get; set; }
        public virtual DbSet<ClassDiagram> ClassDiagrams { get; set; }
        public virtual DbSet<ClassMethod> ClassMethods { get; set; }
        public virtual DbSet<ClassMethodContent> ClassMethodContents { get; set; }
        public virtual DbSet<ClassMethodParameter> ClassMethodParameters { get; set; }
        public virtual DbSet<ClassMethodParameterContent> ClassMethodParameterContents { get; set; }
        public virtual DbSet<ClassProperty> ClassProperties { get; set; }
        public virtual DbSet<ClassPropertyContent> ClassPropertyContents { get; set; }
        public virtual DbSet<ClassRelationship> ClassRelationships { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueComment> IssueComments { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectContent> ProjectContents { get; set; }
        public virtual DbSet<Requirement> Requirements { get; set; }
        public virtual DbSet<RequirementContent> RequirementContents { get; set; }
        public virtual DbSet<RequirementRisk> RequirementRisks { get; set; }
        public virtual DbSet<SpecificationItem> SpecificationItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCase> UserCases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ActorMapping());
            modelBuilder.Configurations.Add(new ClassMapping());
            modelBuilder.Configurations.Add(new ClassContentMapping());
            modelBuilder.Configurations.Add(new ClassDiagramMapping());
            modelBuilder.Configurations.Add(new ClassMethodMapping());
            modelBuilder.Configurations.Add(new ClassMethodContentMapping());
            modelBuilder.Configurations.Add(new ClassMethodParameterMapping());
            modelBuilder.Configurations.Add(new ClassMethodParameterContentMapping());
            modelBuilder.Configurations.Add(new ClassPropertyMapping());
            modelBuilder.Configurations.Add(new ClassPropertyContentMapping());
            modelBuilder.Configurations.Add(new ClassRelationshipMapping());
            modelBuilder.Configurations.Add(new ContactMapping());
            modelBuilder.Configurations.Add(new IssueMapping());
            modelBuilder.Configurations.Add(new IssueCommentMapping());

            modelBuilder.Configurations.Add(new ProfileMapping());
            
            modelBuilder.Configurations.Add(new PackageMapping());
            modelBuilder.Configurations.Add(new PackageContentMapping());
            modelBuilder.Configurations.Add(new ProjectMapping());
            modelBuilder.Configurations.Add(new ProjectContentMapping());
            modelBuilder.Configurations.Add(new RequirementMapping());
            modelBuilder.Configurations.Add(new RequirementContentMapping());
            modelBuilder.Configurations.Add(new RequirementRiskMapping());
            modelBuilder.Configurations.Add(new SpecificationItemMapping());
            modelBuilder.Configurations.Add(new UserMapping());

            modelBuilder.Configurations.Add(new UserCaseMapping());
        }
    }
}
