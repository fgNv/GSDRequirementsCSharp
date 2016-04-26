namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classdiagram : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ClassDiagramContent", "name", c => c.String(maxLength: 100, unicode: false));
            DropPrimaryKey("ClassDiagram");
            RenameTable(name: "ClassContent", newName: "ClassDiagramContent");
            DropForeignKey("ClassContent", "id", "Class");
            DropForeignKey("ClassMethodContent", "id", "ClassMethod");
            DropForeignKey("ClassMethodParameterContent", "id", "ClassMethodParameter");
            DropForeignKey("ClassPropertyContent", "id", "ClassProperty");
            DropIndex("ClassDiagramContent", new[] { "id" });
            DropIndex("ClassMethodContent", new[] { "id" });
            DropIndex("ClassMethodParameterContent", new[] { "id" });
            DropIndex("ClassPropertyContent", new[] { "id" });
            AddColumn("ClassDiagramContent", "ClassDiagram_Id", c => c.Guid(nullable: false));
            AddColumn("ClassDiagramContent", "ClassDiagram_Version", c => c.Int(nullable: false));
            AddColumn("Class", "type", c => c.Int(nullable: false));
            AddColumn("Class", "name", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AddColumn("ClassMethod", "name", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AddColumn("ClassMethodParameter", "name", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AddColumn("ClassProperty", "name", c => c.String(unicode: false));
            AddColumn("ClassDiagram", "version", c => c.Int(nullable: false));
            AddColumn("ClassDiagram", "identifier", c => c.Int(nullable: false));
            AddColumn("ClassDiagram", "is_last_version", c => c.Boolean(nullable: false));
            AddColumn("ClassDiagram", "project_id", c => c.Guid(nullable: false));
            AddColumn("ClassDiagram", "active", c => c.Boolean(nullable: false));
            AddPrimaryKey("ClassDiagram", new[] { "id", "version" });
            CreateIndex("ClassDiagram", "project_id");
            CreateIndex("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            AddForeignKey("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram", new[] { "id", "version" }, cascadeDelete: true);
            AddForeignKey("ClassDiagram", "project_id", "Project", "id", cascadeDelete: true);
            DropColumn("ClassDiagram", "locale");
            DropColumn("ClassDiagram", "name");
            DropTable("ClassMethodContent");
            DropTable("ClassMethodParameterContent");
            DropTable("ClassPropertyContent");
        }
        
        public override void Down()
        {
            CreateTable(
                "ClassPropertyContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                ;
            
            CreateTable(
                "ClassMethodParameterContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                ;
            
            CreateTable(
                "ClassMethodContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        Locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.Locale })                ;
            
            AddColumn("ClassDiagram", "name", c => c.String(maxLength: 100, unicode: false));
            AddColumn("ClassDiagram", "locale", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropForeignKey("ClassDiagram", "project_id", "Project");
            DropForeignKey("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropIndex("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropIndex("ClassDiagram", new[] { "project_id" });
            DropPrimaryKey("ClassDiagram");
            DropColumn("ClassDiagram", "active");
            DropColumn("ClassDiagram", "project_id");
            DropColumn("ClassDiagram", "is_last_version");
            DropColumn("ClassDiagram", "identifier");
            DropColumn("ClassDiagram", "version");
            DropColumn("ClassProperty", "name");
            DropColumn("ClassMethodParameter", "name");
            DropColumn("ClassMethod", "name");
            DropColumn("Class", "name");
            DropColumn("Class", "type");
            DropColumn("ClassDiagramContent", "ClassDiagram_Version");
            DropColumn("ClassDiagramContent", "ClassDiagram_Id");
            CreateIndex("ClassPropertyContent", "id");
            CreateIndex("ClassMethodParameterContent", "id");
            CreateIndex("ClassMethodContent", "id");
            CreateIndex("ClassDiagramContent", "id");
            AddForeignKey("ClassPropertyContent", "id", "ClassProperty", "id");
            AddForeignKey("ClassMethodParameterContent", "id", "ClassMethodParameter", "id");
            AddForeignKey("ClassMethodContent", "id", "ClassMethod", "id");
            AddForeignKey("ClassContent", "id", "Class", "id");
            RenameTable(name: "ClassDiagramContent", newName: "ClassContent");
            AddPrimaryKey("ClassDiagram", new[] { "id", "locale" });
            AlterColumn("ClassDiagramContent", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
