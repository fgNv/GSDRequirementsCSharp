namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.id, t.locale });
            
            CreateTable(
                "dbo.ClassContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Class", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        visibility = c.Int(nullable: false),
                        class_diagram_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ClassMethod",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        return_type = c.String(nullable: false, maxLength: 100, unicode: false),
                        visibility = c.Int(nullable: false),
                        class_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Class", t => t.class_id)
                .Index(t => t.class_id);
            
            CreateTable(
                "dbo.ClassMethodContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.ClassMethod", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.ClassMethodParameter",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        class_method_id = c.Guid(nullable: false),
                        type = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ClassMethod", t => t.class_method_id)
                .Index(t => t.class_method_id);
            
            CreateTable(
                "dbo.ClassMethodParameterContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.ClassMethodParameter", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.ClassProperty",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        class_id = c.Guid(nullable: false),
                        visibility = c.Int(nullable: false),
                        type = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Class", t => t.class_id)
                .Index(t => t.class_id);
            
            CreateTable(
                "dbo.ClassPropertyContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.ClassProperty", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.ClassDiagram",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.SpecificationItem", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.SpecificationItem",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        package_id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Issue",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        concluded = c.Boolean(nullable: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        specification_item_id = c.Guid(nullable: false),
                        creator_id = c.Guid(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.creator_id)
                .ForeignKey("dbo.SpecificationItem", t => t.specification_item_id)
                .Index(t => t.specification_item_id)
                .Index(t => t.creator_id);
            
            CreateTable(
                "dbo.IssueComment",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        content = c.String(nullable: false, unicode: false, storeType: "text"),
                        issue_id = c.Guid(nullable: false),
                        creator_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.creator_id)
                .ForeignKey("dbo.Issue", t => t.issue_id)
                .Index(t => t.issue_id)
                .Index(t => t.creator_id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        login = c.String(nullable: false, maxLength: 50, unicode: false),
                        password = c.String(nullable: false, maxLength: 50, unicode: false),
                        contactId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Contact", t => t.contactId)
                .Index(t => t.contactId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        email = c.String(nullable: false, maxLength: 100, unicode: false),
                        mobilePhone = c.String(maxLength: 20, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        phone = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Requirement",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        rank = c.Int(nullable: false),
                        difficulty = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        creator_id = c.Guid(nullable: false),
                        contact_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Contact", t => t.contact_id)
                .ForeignKey("dbo.User", t => t.creator_id)
                .ForeignKey("dbo.SpecificationItem", t => t.id)
                .Index(t => t.id)
                .Index(t => t.creator_id)
                .Index(t => t.contact_id);
            
            CreateTable(
                "dbo.RequirementContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        action = c.String(maxLength: 150, unicode: false),
                        condition = c.String(maxLength: 150, unicode: false),
                        subject = c.String(maxLength: 150, unicode: false),
                        creator_id = c.Guid(),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Requirement", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.RequirementRisk",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        requirement_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Requirement", t => t.requirement_id)
                .Index(t => t.requirement_id);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        project_id = c.Guid(nullable: false),
                        name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Project", t => t.project_id)
                .Index(t => t.project_id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        owner_id = c.Guid(nullable: false),
                        creator_id = c.Guid(nullable: false),
                        created_at = c.DateTime(nullable: false, precision: 0),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.owner_id)
                .Index(t => t.owner_id);
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        project_id = c.Guid(nullable: false),
                        description = c.String(unicode: false, storeType: "text"),
                        creator_id = c.Guid(nullable: false),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Project", t => t.project_id)
                .Index(t => t.project_id);
            
            CreateTable(
                "dbo.ProjectContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        project_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Project", t => t.project_id)
                .Index(t => t.project_id);
            
            CreateTable(
                "dbo.UserCase",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        actor_id = c.Guid(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.SpecificationItem", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.ClassRelationship",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        source_multiplicity = c.String(maxLength: 10, unicode: false),
                        target_multiplicity = c.String(maxLength: 10, unicode: false),
                        source_id = c.Guid(nullable: false),
                        target_id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.User_Profile",
                c => new
                    {
                        profile_id = c.Guid(nullable: false),
                        user_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.profile_id, t.user_id })
                .ForeignKey("dbo.Profile", t => t.profile_id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.user_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCase", "id", "dbo.SpecificationItem");
            DropForeignKey("dbo.Requirement", "id", "dbo.SpecificationItem");
            DropForeignKey("dbo.Issue", "specification_item_id", "dbo.SpecificationItem");
            DropForeignKey("dbo.IssueComment", "issue_id", "dbo.Issue");
            DropForeignKey("dbo.Requirement", "creator_id", "dbo.User");
            DropForeignKey("dbo.Project", "owner_id", "dbo.User");
            DropForeignKey("dbo.User_Profile", "user_id", "dbo.User");
            DropForeignKey("dbo.User_Profile", "profile_id", "dbo.Profile");
            DropForeignKey("dbo.ProjectContent", "project_id", "dbo.Project");
            DropForeignKey("dbo.Profile", "project_id", "dbo.Project");
            DropForeignKey("dbo.Package", "project_id", "dbo.Project");
            DropForeignKey("dbo.Issue", "creator_id", "dbo.User");
            DropForeignKey("dbo.IssueComment", "creator_id", "dbo.User");
            DropForeignKey("dbo.User", "contactId", "dbo.Contact");
            DropForeignKey("dbo.Requirement", "contact_id", "dbo.Contact");
            DropForeignKey("dbo.RequirementRisk", "requirement_id", "dbo.Requirement");
            DropForeignKey("dbo.RequirementContent", "id", "dbo.Requirement");
            DropForeignKey("dbo.ClassDiagram", "id", "dbo.SpecificationItem");
            DropForeignKey("dbo.ClassProperty", "class_id", "dbo.Class");
            DropForeignKey("dbo.ClassPropertyContent", "id", "dbo.ClassProperty");
            DropForeignKey("dbo.ClassMethod", "class_id", "dbo.Class");
            DropForeignKey("dbo.ClassMethodParameter", "class_method_id", "dbo.ClassMethod");
            DropForeignKey("dbo.ClassMethodParameterContent", "id", "dbo.ClassMethodParameter");
            DropForeignKey("dbo.ClassMethodContent", "id", "dbo.ClassMethod");
            DropForeignKey("dbo.ClassContent", "id", "dbo.Class");
            DropIndex("dbo.User_Profile", new[] { "user_id" });
            DropIndex("dbo.User_Profile", new[] { "profile_id" });
            DropIndex("dbo.UserCase", new[] { "id" });
            DropIndex("dbo.ProjectContent", new[] { "project_id" });
            DropIndex("dbo.Package", new[] { "project_id" });
            DropIndex("dbo.Project", new[] { "owner_id" });
            DropIndex("dbo.Profile", new[] { "project_id" });
            DropIndex("dbo.RequirementRisk", new[] { "requirement_id" });
            DropIndex("dbo.RequirementContent", new[] { "id" });
            DropIndex("dbo.Requirement", new[] { "contact_id" });
            DropIndex("dbo.Requirement", new[] { "creator_id" });
            DropIndex("dbo.Requirement", new[] { "id" });
            DropIndex("dbo.User", new[] { "contactId" });
            DropIndex("dbo.IssueComment", new[] { "creator_id" });
            DropIndex("dbo.IssueComment", new[] { "issue_id" });
            DropIndex("dbo.Issue", new[] { "creator_id" });
            DropIndex("dbo.Issue", new[] { "specification_item_id" });
            DropIndex("dbo.ClassDiagram", new[] { "id" });
            DropIndex("dbo.ClassPropertyContent", new[] { "id" });
            DropIndex("dbo.ClassProperty", new[] { "class_id" });
            DropIndex("dbo.ClassMethodParameterContent", new[] { "id" });
            DropIndex("dbo.ClassMethodParameter", new[] { "class_method_id" });
            DropIndex("dbo.ClassMethodContent", new[] { "id" });
            DropIndex("dbo.ClassMethod", new[] { "class_id" });
            DropIndex("dbo.ClassContent", new[] { "id" });
            DropTable("dbo.User_Profile");
            DropTable("dbo.ClassRelationship");
            DropTable("dbo.UserCase");
            DropTable("dbo.ProjectContent");
            DropTable("dbo.Package");
            DropTable("dbo.Project");
            DropTable("dbo.Profile");
            DropTable("dbo.RequirementRisk");
            DropTable("dbo.RequirementContent");
            DropTable("dbo.Requirement");
            DropTable("dbo.Contact");
            DropTable("dbo.User");
            DropTable("dbo.IssueComment");
            DropTable("dbo.Issue");
            DropTable("dbo.SpecificationItem");
            DropTable("dbo.ClassDiagram");
            DropTable("dbo.ClassPropertyContent");
            DropTable("dbo.ClassProperty");
            DropTable("dbo.ClassMethodParameterContent");
            DropTable("dbo.ClassMethodParameter");
            DropTable("dbo.ClassMethodContent");
            DropTable("dbo.ClassMethod");
            DropTable("dbo.Class");
            DropTable("dbo.ClassContent");
            DropTable("dbo.Actor");
        }
    }
}
