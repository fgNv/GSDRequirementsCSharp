namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issuestranslations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        is_updated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.Issue", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.IssueCommentContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        is_updated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })
                .ForeignKey("dbo.IssueComment", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            AddColumn("dbo.Issue", "identifier", c => c.Int(nullable: false));
            AddColumn("dbo.Issue", "created_at", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Issue", "last_modification", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Issue", "concluded_at", c => c.DateTime(precision: 0));
            AddColumn("dbo.Issue", "project_id", c => c.Guid(nullable: false));
            AddColumn("dbo.IssueComment", "created_at", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.IssueComment", "last_modification", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.IssueComment", "active", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Issue", "project_id");
            AddForeignKey("dbo.Issue", "project_id", "dbo.Project", "id", cascadeDelete: true);
            DropColumn("dbo.Issue", "description");
            DropColumn("dbo.IssueComment", "content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IssueComment", "content", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AddColumn("dbo.Issue", "description", c => c.String(nullable: false, unicode: false, storeType: "text"));
            DropForeignKey("dbo.Issue", "project_id", "dbo.Project");
            DropForeignKey("dbo.IssueCommentContent", "id", "dbo.IssueComment");
            DropForeignKey("dbo.IssueContent", "id", "dbo.Issue");
            DropIndex("dbo.IssueCommentContent", new[] { "id" });
            DropIndex("dbo.IssueContent", new[] { "id" });
            DropIndex("dbo.Issue", new[] { "project_id" });
            DropColumn("dbo.IssueComment", "active");
            DropColumn("dbo.IssueComment", "last_modification");
            DropColumn("dbo.IssueComment", "created_at");
            DropColumn("dbo.Issue", "project_id");
            DropColumn("dbo.Issue", "concluded_at");
            DropColumn("dbo.Issue", "last_modification");
            DropColumn("dbo.Issue", "created_at");
            DropColumn("dbo.Issue", "identifier");
            DropTable("dbo.IssueCommentContent");
            DropTable("dbo.IssueContent");
        }
    }
}
