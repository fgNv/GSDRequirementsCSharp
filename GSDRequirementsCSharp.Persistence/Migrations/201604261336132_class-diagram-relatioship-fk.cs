namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classdiagramrelatioshipfk : DbMigration
    {
        public override void Up()
        {
            AddColumn("Class", "ClassDiagram_Id", c => c.Guid(nullable: false));
            AddColumn("Class", "ClassDiagram_Version", c => c.Int(nullable: false));
            AddColumn("ClassRelationship", "ClassDiagram_Id", c => c.Guid(nullable: false));
            AddColumn("ClassRelationship", "ClassDiagram_Version", c => c.Int(nullable: false));
            CreateIndex("Class", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            CreateIndex("ClassRelationship", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            AddForeignKey("Class", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram", new[] { "id", "version" }, cascadeDelete: true);
            AddForeignKey("ClassRelationship", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram", new[] { "id", "version" }, cascadeDelete: true);
            DropColumn("Class", "class_diagram_id");
        }
        
        public override void Down()
        {
            AddColumn("Class", "class_diagram_id", c => c.Guid(nullable: false));
            DropForeignKey("ClassRelationship", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropForeignKey("Class", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropIndex("ClassRelationship", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropIndex("Class", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropColumn("ClassRelationship", "ClassDiagram_Version");
            DropColumn("ClassRelationship", "ClassDiagram_Id");
            DropColumn("Class", "ClassDiagram_Version");
            DropColumn("Class", "ClassDiagram_Id");
        }
    }
}
