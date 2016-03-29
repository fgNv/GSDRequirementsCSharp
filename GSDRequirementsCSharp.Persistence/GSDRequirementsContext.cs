namespace GSDRequirementsCSharp.Persistence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Common;
    using Domain;
    using Mappings;
    public partial class GSDRequirementsContext : DbContext
    {
        public GSDRequirementsContext() : base(GetDbConnection(), true)
        {
            
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
            modelBuilder.Entity<Actor>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.ClassContents)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.ClassMethods)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.class_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.ClassProperties)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.class_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassContent>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<ClassContent>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ClassDiagram>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ClassDiagram>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMethod>()
                .Property(e => e.return_type)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMethod>()
                .HasMany(e => e.ClassMethodContents)
                .WithRequired(e => e.ClassMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassMethod>()
                .HasMany(e => e.ClassMethodParameters)
                .WithRequired(e => e.ClassMethod)
                .HasForeignKey(e => e.class_method_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassMethodContent>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMethodContent>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMethodParameter>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMethodParameter>()
                .HasMany(e => e.ClassMethodParameterContents)
                .WithRequired(e => e.ClassMethodParameter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassMethodParameterContent>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<ClassMethodParameterContent>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ClassProperty>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<ClassProperty>()
                .HasMany(e => e.ClassPropertyContents)
                .WithRequired(e => e.ClassProperty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassPropertyContent>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<ClassPropertyContent>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ClassRelationship>()
                .Property(e => e.source_multiplicity)
                .IsUnicode(false);

            modelBuilder.Entity<ClassRelationship>()
                .Property(e => e.target_multiplicity)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.mobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Requirements)
                .WithRequired(e => e.Contact)
                .HasForeignKey(e => e.contact_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Contact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Issue>()
                .HasMany(e => e.IssueComments)
                .WithRequired(e => e.Issue)
                .HasForeignKey(e => e.issue_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IssueComment>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Package>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Profiles)
                .Map(m => m.ToTable("User_Profile", "gsd_requirements").MapLeftKey("profile_id").MapRightKey("user_id"));

            modelBuilder.Configurations.Add(new ActorMapping());
            modelBuilder.Configurations.Add(new ProjectMapping());
            modelBuilder.Configurations.Add(new ProjectContentMapping());
            
            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementContents)
                .WithRequired(e => e.Requirement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementRisks)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequirementContent>()
                .Property(e => e.locale)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementContent>()
                .Property(e => e.action)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementContent>()
                .Property(e => e.condition)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementContent>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementRisk>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<SpecificationItem>()
                .HasMany(e => e.ClassDiagrams)
                .WithRequired(e => e.SpecificationItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpecificationItem>()
                .HasMany(e => e.Issues)
                .WithRequired(e => e.SpecificationItem)
                .HasForeignKey(e => e.specification_item_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpecificationItem>()
                .HasOptional(e => e.Requirement)
                .WithRequired(e => e.SpecificationItem);

            modelBuilder.Entity<SpecificationItem>()
                .HasOptional(e => e.UserCase)
                .WithRequired(e => e.SpecificationItem);

            modelBuilder.Entity<User>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Issues)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.IssueComments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.creator_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Requirements)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.creator_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserCase>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<UserCase>()
                .Property(e => e.description)
                .IsUnicode(false);
        }
    }
}
