namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SequenceDiagram : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SequenceDiagram",
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
                .Index(t => new { t.identifier, t.project_id, t.version }, unique: true, name: "requirement_identifier")
                .Index(t => t.is_last_version, name: "IX_Requirement_Last_Version");
            
            CreateTable(
                "SequenceDiagramContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        version = c.Int(nullable: false),
                        description = c.String(unicode: false),
                        creator_id = c.Int(),
                    })
                .PrimaryKey(t => new { t.id, t.locale, t.version })                
                .ForeignKey("User", t => t.creator_id)
                .ForeignKey("SequenceDiagram", t => new { t.id, t.version })
                .Index(t => new { t.id, t.version })
                .Index(t => t.creator_id);
            
            AlterColumn("RequirementContent", "creator_id", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("SequenceDiagram", "id", "SpecificationItem");
            DropForeignKey("SequenceDiagram", "project_id", "Project");
            DropForeignKey("SequenceDiagramContent", new[] { "id", "version" }, "SequenceDiagram");
            DropForeignKey("SequenceDiagramContent", "creator_id", "User");
            DropIndex("SequenceDiagramContent", new[] { "creator_id" });
            DropIndex("SequenceDiagramContent", new[] { "id", "version" });
            DropIndex("SequenceDiagram", "IX_Requirement_Last_Version");
            DropIndex("SequenceDiagram", "requirement_identifier");
            DropIndex("SequenceDiagram", new[] { "id" });
            AlterColumn("RequirementContent", "creator_id", c => c.Guid());
            DropTable("SequenceDiagramContent");
            DropTable("SequenceDiagram");
        }
    }
}
