namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requirementidentifier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requirement", "identifier", c => c.Int(nullable: false));
            AddColumn("dbo.Requirement", "project_id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Requirement", new[] { "identifier", "type", "project_id", "version" }, unique: true, name: "requirement_identifier");
            AddForeignKey("dbo.Requirement", "project_id", "dbo.Project", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requirement", "project_id", "dbo.Project");
            DropIndex("dbo.Requirement", "requirement_identifier");
            DropColumn("dbo.Requirement", "project_id");
            DropColumn("dbo.Requirement", "identifier");
        }
    }
}
