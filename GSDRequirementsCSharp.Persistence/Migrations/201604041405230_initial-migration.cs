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
                .ForeignKey("dbo.SpecificationItem", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.SpecificationItem",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        package_id = c.Guid(nullable: false),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Package", t => t.package_id, cascadeDelete: true)
                .Index(t => t.package_id);
            
            CreateTable(
                "dbo.Issue",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        concluded = c.Boolean(nullable: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        specification_item_id = c.Guid(nullable: false),
                        creator_id = c.Int(),
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
                        creator_id = c.Int(nullable: false),
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
                        id = c.Int(nullable: false, identity: true),
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
                        version = c.Int(nullable: false),
                        identifier = c.Int(nullable: false),
                        is_last_version = c.Boolean(nullable: false),
                        Rank = c.Int(nullable: false),
                        difficulty = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        ContactId = c.Guid(),
                        project_id = c.Guid(nullable: false),
                        creator_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })
                .ForeignKey("dbo.Contact", t => t.ContactId)
                .ForeignKey("dbo.Project", t => t.project_id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.creator_id)
                .ForeignKey("dbo.SpecificationItem", t => t.id, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => new { t.identifier, t.type, t.project_id, t.version }, unique: true, name: "requirement_identifier")
                .Index(t => t.ContactId)
                .Index(t => t.creator_id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        owner_id = c.Int(nullable: false),
                        creator_id = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false, precision: 0),
                        active = c.Boolean(nullable: false),
                        identifier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.owner_id)
                .Index(t => t.owner_id)
                .Index(t => new { t.identifier, t.creator_id }, unique: true, name: "project_identifier");
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        project_id = c.Guid(nullable: false),
                        creator_id = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        identifier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Project", t => t.project_id)
                .Index(t => new { t.identifier, t.project_id }, unique: true, name: "package_identifier");
            
            CreateTable(
                "dbo.PackageContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        is_updated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Package", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
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
                "dbo.ProjectContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        project_id = c.Guid(nullable: false),
                        IsUpdated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Project", t => t.project_id)
                .Index(t => t.project_id);
            
            CreateTable(
                "dbo.RequirementContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        version = c.Int(nullable: false),
                        action = c.String(maxLength: 150, unicode: false),
                        condition = c.String(maxLength: 150, unicode: false),
                        subject = c.String(maxLength: 150, unicode: false),
                        creator_id = c.Guid(),
                    })
                .PrimaryKey(t => new { t.id, t.locale, t.version })
                .ForeignKey("dbo.Requirement", t => new { t.id, t.version })
                .Index(t => new { t.id, t.version });
            
            CreateTable(
                "dbo.RequirementRisk",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        Requirement_Id = c.Guid(nullable: false),
                        Requirement_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Requirement", t => new { t.Requirement_Id, t.Requirement_Version })
                .Index(t => new { t.Requirement_Id, t.Requirement_Version });
            
            CreateTable(
                "dbo.UserCase",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        actor_id = c.Guid(),
                        SpecificationItem_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.SpecificationItem", t => t.SpecificationItem_Id, cascadeDelete: true)
                .Index(t => t.SpecificationItem_Id);
            
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
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.profile_id, t.user_id })
                .ForeignKey("dbo.Profile", t => t.profile_id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.user_id, cascadeDelete: true)
                .Index(t => t.profile_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCase", "SpecificationItem_Id", "dbo.SpecificationItem");
            DropForeignKey("dbo.Requirement", "id", "dbo.SpecificationItem");
            DropForeignKey("dbo.SpecificationItem", "package_id", "dbo.Package");
            DropForeignKey("dbo.Issue", "specification_item_id", "dbo.SpecificationItem");
            DropForeignKey("dbo.IssueComment", "issue_id", "dbo.Issue");
            DropForeignKey("dbo.Requirement", "creator_id", "dbo.User");
            DropForeignKey("dbo.Project", "owner_id", "dbo.User");
            DropForeignKey("dbo.Issue", "creator_id", "dbo.User");
            DropForeignKey("dbo.IssueComment", "creator_id", "dbo.User");
            DropForeignKey("dbo.User", "contactId", "dbo.Contact");
            DropForeignKey("dbo.RequirementRisk", new[] { "Requirement_Id", "Requirement_Version" }, "dbo.Requirement");
            DropForeignKey("dbo.RequirementContent", new[] { "id", "version" }, "dbo.Requirement");
            DropForeignKey("dbo.Requirement", "project_id", "dbo.Project");
            DropForeignKey("dbo.ProjectContent", "project_id", "dbo.Project");
            DropForeignKey("dbo.Profile", "project_id", "dbo.Project");
            DropForeignKey("dbo.User_Profile", "user_id", "dbo.User");
            DropForeignKey("dbo.User_Profile", "profile_id", "dbo.Profile");
            DropForeignKey("dbo.Package", "project_id", "dbo.Project");
            DropForeignKey("dbo.PackageContent", "id", "dbo.Package");
            DropForeignKey("dbo.Requirement", "ContactId", "dbo.Contact");
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
            DropIndex("dbo.UserCase", new[] { "SpecificationItem_Id" });
            DropIndex("dbo.RequirementRisk", new[] { "Requirement_Id", "Requirement_Version" });
            DropIndex("dbo.RequirementContent", new[] { "id", "version" });
            DropIndex("dbo.ProjectContent", new[] { "project_id" });
            DropIndex("dbo.Profile", new[] { "project_id" });
            DropIndex("dbo.PackageContent", new[] { "id" });
            DropIndex("dbo.Package", "package_identifier");
            DropIndex("dbo.Project", "project_identifier");
            DropIndex("dbo.Project", new[] { "owner_id" });
            DropIndex("dbo.Requirement", new[] { "creator_id" });
            DropIndex("dbo.Requirement", new[] { "ContactId" });
            DropIndex("dbo.Requirement", "requirement_identifier");
            DropIndex("dbo.Requirement", new[] { "id" });
            DropIndex("dbo.User", new[] { "contactId" });
            DropIndex("dbo.IssueComment", new[] { "creator_id" });
            DropIndex("dbo.IssueComment", new[] { "issue_id" });
            DropIndex("dbo.Issue", new[] { "creator_id" });
            DropIndex("dbo.Issue", new[] { "specification_item_id" });
            DropIndex("dbo.SpecificationItem", new[] { "package_id" });
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
            DropTable("dbo.RequirementRisk");
            DropTable("dbo.RequirementContent");
            DropTable("dbo.ProjectContent");
            DropTable("dbo.Profile");
            DropTable("dbo.PackageContent");
            DropTable("dbo.Package");
            DropTable("dbo.Project");
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
