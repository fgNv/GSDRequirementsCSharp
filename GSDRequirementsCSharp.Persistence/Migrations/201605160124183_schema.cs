namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "UseCaseEntity",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        UseCaseDiagram_Id = c.Guid(nullable: false),
                        UseCaseDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version }, cascadeDelete: true)
                .Index(t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version });
            
            CreateTable(
                "ActorContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Actor_Id = c.Guid(nullable: false),
                        Actor_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("Actor", t => new { t.Actor_Id, t.Actor_Version })
                .Index(t => new { t.Actor_Id, t.Actor_Version });
            
            CreateTable(
                "UseCaseDiagram",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        project_id = c.Guid(nullable: false),
                        identifier = c.Int(nullable: false),
                        is_last_version = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .ForeignKey("SpecificationItem", t => t.id, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => new { t.identifier, t.project_id, t.version }, unique: true, name: "use_case_diagram_identifier");
            
            CreateTable(
                "UseCaseDiagramContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        UseCaseDiagram_Id = c.Guid(nullable: false),
                        UseCaseDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version }, cascadeDelete: true)
                .Index(t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version });
            
            CreateTable(
                "UseCaseContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        path = c.String(unicode: false, storeType: "text"),
                        description = c.String(unicode: false, storeType: "text"),
                        UseCase_Id = c.Guid(nullable: false),
                        UseCase_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCase", t => new { t.UseCase_Id, t.UseCase_Version })
                .Index(t => new { t.UseCase_Id, t.UseCase_Version });
            
            CreateTable(
                "UseCasePostCondition",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        UseCase_Id = c.Guid(nullable: false),
                        UseCase_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCase", t => new { t.UseCase_Id, t.UseCase_Version })
                .Index(t => new { t.UseCase_Id, t.UseCase_Version });
            
            CreateTable(
                "UseCasePostConditionContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        description = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCasePostCondition", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "UseCasePreCondition",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        UseCase_Id = c.Guid(nullable: false),
                        UseCase_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCase", t => new { t.UseCase_Id, t.UseCase_Version })
                .Index(t => new { t.UseCase_Id, t.UseCase_Version });
            
            CreateTable(
                "UseCasePreConditionContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        description = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCasePreCondition", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "Project",
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
                .ForeignKey("User", t => t.owner_id)
                .Index(t => t.owner_id)
                .Index(t => new { t.identifier, t.creator_id }, unique: true, name: "project_identifier");
            
            CreateTable(
                "User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 50, unicode: false),
                        password = c.String(nullable: false, maxLength: 50, unicode: false),
                        contactId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Contact", t => t.contactId)
                .Index(t => t.login, unique: true, name: "login_unique")
                .Index(t => t.contactId);
            
            CreateTable(
                "Contact",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        email = c.String(nullable: false, maxLength: 100, unicode: false),
                        mobilePhone = c.String(maxLength: 20, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        phone = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id)                
                .Index(t => t.email, unique: true, name: "email_unique");
            
            CreateTable(
                "Requirement",
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
                .ForeignKey("Contact", t => t.ContactId)
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .ForeignKey("SpecificationItem", t => t.id, cascadeDelete: true)
                .ForeignKey("User", t => t.creator_id)
                .Index(t => t.id)
                .Index(t => new { t.identifier, t.type, t.project_id, t.version }, unique: true, name: "requirement_identifier")
                .Index(t => t.is_last_version, name: "IX_Requirement_Last_Version")
                .Index(t => t.ContactId)
                .Index(t => t.creator_id);
            
            CreateTable(
                "RequirementContent",
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
                .ForeignKey("Requirement", t => new { t.id, t.version })
                .Index(t => new { t.id, t.version });
            
            CreateTable(
                "RequirementRisk",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        Requirement_Id = c.Guid(nullable: false),
                        Requirement_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Requirement", t => new { t.Requirement_Id, t.Requirement_Version })
                .Index(t => new { t.Requirement_Id, t.Requirement_Version });
            
            CreateTable(
                "SpecificationItem",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        package_id = c.Guid(nullable: false),
                        active = c.Boolean(nullable: false),
                        label = c.String(unicode: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Package", t => t.package_id, cascadeDelete: true)
                .Index(t => t.package_id)
                .Index(t => t.active, name: "IX_Specification_Active");
            
            CreateTable(
                "Issue",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        identifier = c.Int(nullable: false),
                        concluded = c.Boolean(nullable: false),
                        created_at = c.DateTime(nullable: false, precision: 0),
                        last_modification = c.DateTime(nullable: false, precision: 0),
                        concluded_at = c.DateTime(precision: 0),
                        project_id = c.Guid(nullable: false),
                        specification_item_id = c.Guid(nullable: false),
                        creator_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .ForeignKey("SpecificationItem", t => t.specification_item_id)
                .ForeignKey("User", t => t.creator_id)
                .Index(t => t.concluded, name: "IX_Issue_Concluded")
                .Index(t => t.project_id)
                .Index(t => t.specification_item_id)
                .Index(t => t.creator_id);
            
            CreateTable(
                "IssueContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        is_updated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("Issue", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "IssueComment",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        issue_id = c.Guid(nullable: false),
                        creator_id = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false, precision: 0),
                        last_modification = c.DateTime(nullable: false, precision: 0),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Issue", t => t.issue_id)
                .ForeignKey("User", t => t.creator_id)
                .Index(t => t.issue_id)
                .Index(t => t.creator_id);
            
            CreateTable(
                "IssueCommentContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        is_updated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("IssueComment", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "Package",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        project_id = c.Guid(nullable: false),
                        creator_id = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        identifier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Project", t => t.project_id)
                .Index(t => new { t.identifier, t.project_id }, unique: true, name: "package_identifier");
            
            CreateTable(
                "PackageContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        description = c.String(unicode: false, storeType: "text"),
                        is_updated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("Package", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "Permission",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        project_id = c.Guid(nullable: false),
                        profile = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("User", t => t.user_id, cascadeDelete: true)
                .ForeignKey("Project", t => t.project_id)
                .Index(t => t.project_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "ProjectContent",
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
                .ForeignKey("Project", t => t.project_id)
                .Index(t => t.project_id);
            
            CreateTable(
                "UseCaseEntityRelation",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        Source_Version = c.Int(nullable: false),
                        Target_Version = c.Int(nullable: false),
                        Source_Id = c.Guid(nullable: false),
                        Target_Id = c.Guid(nullable: false),
                        use_case_diagram_id = c.Guid(nullable: false),
                        UseCaseDiagram_Id = c.Guid(nullable: false),
                        UseCaseDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCaseEntity", t => new { t.Source_Id, t.Source_Version }, cascadeDelete: true)
                .ForeignKey("UseCaseEntity", t => new { t.Target_Id, t.Target_Version }, cascadeDelete: true)
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version }, cascadeDelete: true)
                .Index(t => new { t.Source_Id, t.Source_Version })
                .Index(t => new { t.Target_Id, t.Target_Version })
                .Index(t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version });
            
            CreateTable(
                "UseCaseEntitiesRelationContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        description = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCaseEntityRelation", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "UseCasesRelation",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        target_id = c.Guid(nullable: false),
                        source_id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        use_case_diagram_id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCase", t => new { t.source_id, t.version })
                .ForeignKey("UseCase", t => new { t.target_id, t.version })
                .ForeignKey("UseCaseDiagram", t => new { t.use_case_diagram_id, t.version }, cascadeDelete: true)
                .Index(t => new { t.target_id, t.version })
                .Index(t => new { t.source_id, t.version })
                .Index(t => new { t.use_case_diagram_id, t.version });
            
            CreateTable(
                "Auditing",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        project_id = c.Guid(nullable: false),
                        user_id = c.Int(nullable: false),
                        activity_description = c.String(unicode: false),
                        executed_at = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .ForeignKey("User", t => t.user_id, cascadeDelete: true)
                .Index(t => t.project_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "ClassDiagram",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        identifier = c.Int(nullable: false),
                        is_last_version = c.Boolean(nullable: false),
                        project_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .ForeignKey("SpecificationItem", t => t.id, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => new { t.identifier, t.project_id, t.version }, unique: true, name: "class_diagram_identifier");
            
            CreateTable(
                "Class",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                        coordinates_x = c.Int(nullable: false),
                        coordinates_y = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        ClassDiagram_Id = c.Guid(nullable: false),
                        ClassDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("ClassDiagram", t => new { t.ClassDiagram_Id, t.ClassDiagram_Version }, cascadeDelete: true)
                .Index(t => new { t.ClassDiagram_Id, t.ClassDiagram_Version });
            
            CreateTable(
                "ClassMethod",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        return_type = c.String(maxLength: 100, unicode: false),
                        visibility = c.Int(nullable: false),
                        class_id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Class", t => t.class_id)
                .Index(t => t.class_id);
            
            CreateTable(
                "ClassMethodParameter",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        class_method_id = c.Guid(nullable: false),
                        type = c.String(nullable: false, maxLength: 100, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("ClassMethod", t => t.class_method_id)
                .Index(t => t.class_method_id);
            
            CreateTable(
                "ClassProperty",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        class_id = c.Guid(nullable: false),
                        visibility = c.Int(nullable: false),
                        type = c.String(nullable: false, maxLength: 100, unicode: false),
                        name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Class", t => t.class_id)
                .Index(t => t.class_id);
            
            CreateTable(
                "ClassDiagramContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(maxLength: 100, unicode: false),
                        ClassDiagram_Id = c.Guid(nullable: false),
                        ClassDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("ClassDiagram", t => new { t.ClassDiagram_Id, t.ClassDiagram_Version }, cascadeDelete: true)
                .Index(t => new { t.ClassDiagram_Id, t.ClassDiagram_Version });
            
            CreateTable(
                "ClassRelationship",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        source_multiplicity = c.String(maxLength: 10, unicode: false),
                        target_multiplicity = c.String(maxLength: 10, unicode: false),
                        source_id = c.Guid(nullable: false),
                        target_id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                        ClassDiagram_Id = c.Guid(nullable: false),
                        ClassDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("Class", t => t.source_id, cascadeDelete: true)
                .ForeignKey("Class", t => t.target_id, cascadeDelete: true)
                .ForeignKey("ClassDiagram", t => new { t.ClassDiagram_Id, t.ClassDiagram_Version }, cascadeDelete: true)
                .Index(t => t.source_id)
                .Index(t => t.target_id)
                .Index(t => new { t.ClassDiagram_Id, t.ClassDiagram_Version });
            
            CreateTable(
                "SpecificationItemSpecificationItem",
                c => new
                    {
                        SpecificationItem_Id = c.Guid(nullable: false),
                        SpecificationItem_Id1 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpecificationItem_Id, t.SpecificationItem_Id1 })                
                .ForeignKey("SpecificationItem", t => t.SpecificationItem_Id)
                .ForeignKey("SpecificationItem", t => t.SpecificationItem_Id1)
                .Index(t => t.SpecificationItem_Id)
                .Index(t => t.SpecificationItem_Id1);
            
            CreateTable(
                "Actor",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("UseCaseEntity", t => new { t.id, t.version })
                .Index(t => new { t.id, t.version });
            
            CreateTable(
                "UseCase",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        SpecificationItem_Id = c.Guid(nullable: false),
                        identifier = c.Int(nullable: false),
                        project_id = c.Guid(nullable: false),
                        is_last_version = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("UseCaseEntity", t => new { t.id, t.version })
                .ForeignKey("SpecificationItem", t => t.SpecificationItem_Id, cascadeDelete: true)
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .Index(t => new { t.id, t.version })
                .Index(t => new { t.identifier, t.project_id, t.version }, unique: true, name: "use_case_identifier")
                .Index(t => t.SpecificationItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("UseCase", "project_id", "Project");
            DropForeignKey("UseCase", "SpecificationItem_Id", "SpecificationItem");
            DropForeignKey("UseCase", new[] { "id", "version" }, "UseCaseEntity");
            DropForeignKey("Actor", new[] { "id", "version" }, "UseCaseEntity");
            DropForeignKey("ClassDiagram", "id", "SpecificationItem");
            DropForeignKey("ClassRelationship", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropForeignKey("ClassRelationship", "target_id", "Class");
            DropForeignKey("ClassRelationship", "source_id", "Class");
            DropForeignKey("ClassDiagram", "project_id", "Project");
            DropForeignKey("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropForeignKey("Class", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropForeignKey("ClassProperty", "class_id", "Class");
            DropForeignKey("ClassMethod", "class_id", "Class");
            DropForeignKey("ClassMethodParameter", "class_method_id", "ClassMethod");
            DropForeignKey("Auditing", "user_id", "User");
            DropForeignKey("Auditing", "project_id", "Project");
            DropForeignKey("UseCasesRelation", new[] { "use_case_diagram_id", "version" }, "UseCaseDiagram");
            DropForeignKey("UseCasesRelation", new[] { "target_id", "version" }, "UseCase");
            DropForeignKey("UseCasesRelation", new[] { "source_id", "version" }, "UseCase");
            DropForeignKey("UseCaseDiagram", "id", "SpecificationItem");
            DropForeignKey("UseCaseDiagram", "project_id", "Project");
            DropForeignKey("UseCaseEntityRelation", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("UseCaseEntityRelation", new[] { "Target_Id", "Target_Version" }, "UseCaseEntity");
            DropForeignKey("UseCaseEntityRelation", new[] { "Source_Id", "Source_Version" }, "UseCaseEntity");
            DropForeignKey("UseCaseEntitiesRelationContent", "id", "UseCaseEntityRelation");
            DropForeignKey("ProjectContent", "project_id", "Project");
            DropForeignKey("Permission", "project_id", "Project");
            DropForeignKey("Package", "project_id", "Project");
            DropForeignKey("Requirement", "creator_id", "User");
            DropForeignKey("Project", "owner_id", "User");
            DropForeignKey("Permission", "user_id", "User");
            DropForeignKey("Issue", "creator_id", "User");
            DropForeignKey("IssueComment", "creator_id", "User");
            DropForeignKey("User", "contactId", "Contact");
            DropForeignKey("Requirement", "id", "SpecificationItem");
            DropForeignKey("SpecificationItem", "package_id", "Package");
            DropForeignKey("PackageContent", "id", "Package");
            DropForeignKey("SpecificationItemSpecificationItem", "SpecificationItem_Id1", "SpecificationItem");
            DropForeignKey("SpecificationItemSpecificationItem", "SpecificationItem_Id", "SpecificationItem");
            DropForeignKey("Issue", "specification_item_id", "SpecificationItem");
            DropForeignKey("Issue", "project_id", "Project");
            DropForeignKey("IssueComment", "issue_id", "Issue");
            DropForeignKey("IssueCommentContent", "id", "IssueComment");
            DropForeignKey("IssueContent", "id", "Issue");
            DropForeignKey("RequirementRisk", new[] { "Requirement_Id", "Requirement_Version" }, "Requirement");
            DropForeignKey("RequirementContent", new[] { "id", "version" }, "Requirement");
            DropForeignKey("Requirement", "project_id", "Project");
            DropForeignKey("Requirement", "ContactId", "Contact");
            DropForeignKey("UseCasePreCondition", new[] { "UseCase_Id", "UseCase_Version" }, "UseCase");
            DropForeignKey("UseCasePreConditionContent", "id", "UseCasePreCondition");
            DropForeignKey("UseCasePostCondition", new[] { "UseCase_Id", "UseCase_Version" }, "UseCase");
            DropForeignKey("UseCasePostConditionContent", "id", "UseCasePostCondition");
            DropForeignKey("UseCaseContent", new[] { "UseCase_Id", "UseCase_Version" }, "UseCase");
            DropForeignKey("UseCaseEntity", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("UseCaseDiagramContent", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("ActorContent", new[] { "Actor_Id", "Actor_Version" }, "Actor");
            DropIndex("UseCase", new[] { "SpecificationItem_Id" });
            DropIndex("UseCase", "use_case_identifier");
            DropIndex("UseCase", new[] { "id", "version" });
            DropIndex("Actor", new[] { "id", "version" });
            DropIndex("SpecificationItemSpecificationItem", new[] { "SpecificationItem_Id1" });
            DropIndex("SpecificationItemSpecificationItem", new[] { "SpecificationItem_Id" });
            DropIndex("ClassRelationship", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropIndex("ClassRelationship", new[] { "target_id" });
            DropIndex("ClassRelationship", new[] { "source_id" });
            DropIndex("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropIndex("ClassProperty", new[] { "class_id" });
            DropIndex("ClassMethodParameter", new[] { "class_method_id" });
            DropIndex("ClassMethod", new[] { "class_id" });
            DropIndex("Class", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropIndex("ClassDiagram", "class_diagram_identifier");
            DropIndex("ClassDiagram", new[] { "id" });
            DropIndex("Auditing", new[] { "user_id" });
            DropIndex("Auditing", new[] { "project_id" });
            DropIndex("UseCasesRelation", new[] { "use_case_diagram_id", "version" });
            DropIndex("UseCasesRelation", new[] { "source_id", "version" });
            DropIndex("UseCasesRelation", new[] { "target_id", "version" });
            DropIndex("UseCaseEntitiesRelationContent", new[] { "id" });
            DropIndex("UseCaseEntityRelation", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("UseCaseEntityRelation", new[] { "Target_Id", "Target_Version" });
            DropIndex("UseCaseEntityRelation", new[] { "Source_Id", "Source_Version" });
            DropIndex("ProjectContent", new[] { "project_id" });
            DropIndex("Permission", new[] { "user_id" });
            DropIndex("Permission", new[] { "project_id" });
            DropIndex("PackageContent", new[] { "id" });
            DropIndex("Package", "package_identifier");
            DropIndex("IssueCommentContent", new[] { "id" });
            DropIndex("IssueComment", new[] { "creator_id" });
            DropIndex("IssueComment", new[] { "issue_id" });
            DropIndex("IssueContent", new[] { "id" });
            DropIndex("Issue", new[] { "creator_id" });
            DropIndex("Issue", new[] { "specification_item_id" });
            DropIndex("Issue", new[] { "project_id" });
            DropIndex("Issue", "IX_Issue_Concluded");
            DropIndex("SpecificationItem", "IX_Specification_Active");
            DropIndex("SpecificationItem", new[] { "package_id" });
            DropIndex("RequirementRisk", new[] { "Requirement_Id", "Requirement_Version" });
            DropIndex("RequirementContent", new[] { "id", "version" });
            DropIndex("Requirement", new[] { "creator_id" });
            DropIndex("Requirement", new[] { "ContactId" });
            DropIndex("Requirement", "IX_Requirement_Last_Version");
            DropIndex("Requirement", "requirement_identifier");
            DropIndex("Requirement", new[] { "id" });
            DropIndex("Contact", "email_unique");
            DropIndex("User", new[] { "contactId" });
            DropIndex("User", "login_unique");
            DropIndex("Project", "project_identifier");
            DropIndex("Project", new[] { "owner_id" });
            DropIndex("UseCasePreConditionContent", new[] { "id" });
            DropIndex("UseCasePreCondition", new[] { "UseCase_Id", "UseCase_Version" });
            DropIndex("UseCasePostConditionContent", new[] { "id" });
            DropIndex("UseCasePostCondition", new[] { "UseCase_Id", "UseCase_Version" });
            DropIndex("UseCaseContent", new[] { "UseCase_Id", "UseCase_Version" });
            DropIndex("UseCaseDiagramContent", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("UseCaseDiagram", "use_case_diagram_identifier");
            DropIndex("UseCaseDiagram", new[] { "id" });
            DropIndex("ActorContent", new[] { "Actor_Id", "Actor_Version" });
            DropIndex("UseCaseEntity", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropTable("UseCase");
            DropTable("Actor");
            DropTable("SpecificationItemSpecificationItem");
            DropTable("ClassRelationship");
            DropTable("ClassDiagramContent");
            DropTable("ClassProperty");
            DropTable("ClassMethodParameter");
            DropTable("ClassMethod");
            DropTable("Class");
            DropTable("ClassDiagram");
            DropTable("Auditing");
            DropTable("UseCasesRelation");
            DropTable("UseCaseEntitiesRelationContent");
            DropTable("UseCaseEntityRelation");
            DropTable("ProjectContent");
            DropTable("Permission");
            DropTable("PackageContent");
            DropTable("Package");
            DropTable("IssueCommentContent");
            DropTable("IssueComment");
            DropTable("IssueContent");
            DropTable("Issue");
            DropTable("SpecificationItem");
            DropTable("RequirementRisk");
            DropTable("RequirementContent");
            DropTable("Requirement");
            DropTable("Contact");
            DropTable("User");
            DropTable("Project");
            DropTable("UseCasePreConditionContent");
            DropTable("UseCasePreCondition");
            DropTable("UseCasePostConditionContent");
            DropTable("UseCasePostCondition");
            DropTable("UseCaseContent");
            DropTable("UseCaseDiagramContent");
            DropTable("UseCaseDiagram");
            DropTable("ActorContent");
            DropTable("UseCaseEntity");
        }
    }
}
