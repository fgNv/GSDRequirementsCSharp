namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classdiagramrelatedremodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ClassDiagram",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        identifier = c.Int(nullable: false),
                        is_last_version = c.Boolean(nullable: false),
                        project_id = c.Guid(nullable: false),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("SpecificationItem", t => t.id, cascadeDelete: true)
                .ForeignKey("Project", t => t.project_id, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => t.project_id);
            
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
                "Class",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        visibility = c.Int(nullable: false),
                        class_diagram_id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)                ;
            
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
                "ClassRelationship",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        source_multiplicity = c.String(maxLength: 10, unicode: false),
                        target_multiplicity = c.String(maxLength: 10, unicode: false),
                        source_id = c.Guid(nullable: false),
                        target_id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                ;
            
        }
        
        public override void Down()
        {
            DropForeignKey("ClassProperty", "class_id", "Class");
            DropForeignKey("ClassMethod", "class_id", "Class");
            DropForeignKey("ClassMethodParameter", "class_method_id", "ClassMethod");
            DropForeignKey("ClassDiagram", "project_id", "Project");
            DropForeignKey("ClassDiagram", "id", "SpecificationItem");
            DropForeignKey("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" }, "ClassDiagram");
            DropIndex("ClassProperty", new[] { "class_id" });
            DropIndex("ClassMethodParameter", new[] { "class_method_id" });
            DropIndex("ClassMethod", new[] { "class_id" });
            DropIndex("ClassDiagramContent", new[] { "ClassDiagram_Id", "ClassDiagram_Version" });
            DropIndex("ClassDiagram", new[] { "project_id" });
            DropIndex("ClassDiagram", new[] { "id" });
            DropTable("ClassRelationship");
            DropTable("ClassProperty");
            DropTable("ClassMethodParameter");
            DropTable("ClassMethod");
            DropTable("Class");
            DropTable("ClassDiagramContent");
            DropTable("ClassDiagram");
        }
    }
}
