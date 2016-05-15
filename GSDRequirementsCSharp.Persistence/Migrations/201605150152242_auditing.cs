namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auditing : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("Auditing", "user_id", "User");
            DropForeignKey("Auditing", "project_id", "Project");
            DropIndex("Auditing", new[] { "user_id" });
            DropIndex("Auditing", new[] { "project_id" });
            DropTable("Auditing");
        }
    }
}
