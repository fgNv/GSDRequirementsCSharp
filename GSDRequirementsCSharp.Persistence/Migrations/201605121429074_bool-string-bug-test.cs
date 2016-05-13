namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolstringbugtest : DbMigration
    {
        public override void Up()
        {
            DropIndex("Requirement", "IX_Requirement_Last_Version");
            DropIndex("SpecificationItem", "IX_Specification_Active");
            DropIndex("Issue", "IX_Issue_Concluded");
            AlterColumn("UseCase", "is_last_version", c => c.Boolean(nullable: false));
            AlterColumn("UseCaseDiagram", "is_last_version", c => c.Boolean(nullable: false));
            AlterColumn("Project", "active", c => c.Boolean(nullable: false));
            AlterColumn("Requirement", "is_last_version", c => c.Boolean(nullable: false));
            AlterColumn("SpecificationItem", "active", c => c.Boolean(nullable: false));
            AlterColumn("Issue", "concluded", c => c.Boolean(nullable: false));
            AlterColumn("IssueContent", "is_updated", c => c.Boolean(nullable: false));
            AlterColumn("IssueComment", "active", c => c.Boolean(nullable: false));
            AlterColumn("IssueCommentContent", "is_updated", c => c.Boolean(nullable: false));
            AlterColumn("Package", "active", c => c.Boolean(nullable: false));
            AlterColumn("PackageContent", "is_updated", c => c.Boolean(nullable: false));
            AlterColumn("ProjectContent", "IsUpdated", c => c.Boolean(nullable: false));
            AlterColumn("ClassDiagram", "is_last_version", c => c.Boolean(nullable: false));
            CreateIndex("Requirement", "is_last_version", name: "IX_Requirement_Last_Version");
            CreateIndex("SpecificationItem", "active", name: "IX_Specification_Active");
            CreateIndex("Issue", "concluded", name: "IX_Issue_Concluded");
        }
        
        public override void Down()
        {
            DropIndex("Issue", "IX_Issue_Concluded");
            DropIndex("SpecificationItem", "IX_Specification_Active");
            DropIndex("Requirement", "IX_Requirement_Last_Version");
            AlterColumn("ClassDiagram", "is_last_version", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("ProjectContent", "IsUpdated", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("PackageContent", "is_updated", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("Package", "active", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("IssueCommentContent", "is_updated", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("IssueComment", "active", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("IssueContent", "is_updated", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("Issue", "concluded", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("SpecificationItem", "active", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("Requirement", "is_last_version", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("Project", "active", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("UseCaseDiagram", "is_last_version", c => c.Boolean(nullable: false, storeType: "bit"));
            AlterColumn("UseCase", "is_last_version", c => c.Boolean(nullable: false, storeType: "bit"));
            CreateIndex("Issue", "concluded", name: "IX_Issue_Concluded");
            CreateIndex("SpecificationItem", "active", name: "IX_Specification_Active");
            CreateIndex("Requirement", "is_last_version", name: "IX_Requirement_Last_Version");
        }
    }
}
