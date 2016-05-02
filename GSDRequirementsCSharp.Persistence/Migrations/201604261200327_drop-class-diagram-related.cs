namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropclassdiagramrelated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ClassContent", "id", "Class");
            DropForeignKey("ClassMethodContent", "id", "ClassMethod");
            DropForeignKey("ClassMethodParameterContent", "id", "ClassMethodParameter");
            DropForeignKey("ClassMethodParameter", "class_method_id", "ClassMethod");
            DropForeignKey("ClassMethod", "class_id", "Class");
            DropForeignKey("ClassPropertyContent", "id", "ClassProperty");
            DropForeignKey("ClassProperty", "class_id", "Class");
            DropForeignKey("ClassDiagram", "id", "SpecificationItem");
            DropIndex("ClassContent", new[] { "id" });
            DropIndex("ClassMethod", new[] { "class_id" });
            DropIndex("ClassMethodContent", new[] { "id" });
            DropIndex("ClassMethodParameter", new[] { "class_method_id" });
            DropIndex("ClassMethodParameterContent", new[] { "id" });
            DropIndex("ClassProperty", new[] { "class_id" });
            DropIndex("ClassPropertyContent", new[] { "id" });
            DropIndex("ClassDiagram", new[] { "id" });
            DropTable("ClassContent");
            DropTable("Class");
            DropTable("ClassMethod");
            DropTable("ClassMethodContent");
            DropTable("ClassMethodParameter");
            DropTable("ClassMethodParameterContent");
            DropTable("ClassProperty");
            DropTable("ClassPropertyContent");
            DropTable("ClassDiagram");
            DropTable("ClassRelationship");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "ClassDiagram",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale });
            
            CreateTable(
                "ClassPropertyContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale });
            
            CreateTable(
                "ClassProperty",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        class_id = c.Guid(nullable: false),
                        visibility = c.Int(nullable: false),
                        type = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "ClassMethodParameterContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale });
            
            CreateTable(
                "ClassMethodParameter",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        class_method_id = c.Guid(nullable: false),
                        type = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "ClassMethodContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        Locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.Locale });
            
            CreateTable(
                "ClassMethod",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        return_type = c.String(maxLength: 100, unicode: false),
                        visibility = c.Int(nullable: false),
                        class_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "Class",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        visibility = c.Int(nullable: false),
                        class_diagram_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "ClassContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale });
            
            CreateIndex("ClassDiagram", "id");
            CreateIndex("ClassPropertyContent", "id");
            CreateIndex("ClassProperty", "class_id");
            CreateIndex("ClassMethodParameterContent", "id");
            CreateIndex("ClassMethodParameter", "class_method_id");
            CreateIndex("ClassMethodContent", "id");
            CreateIndex("ClassMethod", "class_id");
            CreateIndex("ClassContent", "id");
            AddForeignKey("ClassDiagram", "id", "SpecificationItem", "id", cascadeDelete: true);
            AddForeignKey("ClassProperty", "class_id", "Class", "id");
            AddForeignKey("ClassPropertyContent", "id", "ClassProperty", "id");
            AddForeignKey("ClassMethod", "class_id", "Class", "id");
            AddForeignKey("ClassMethodParameter", "class_method_id", "ClassMethod", "id");
            AddForeignKey("ClassMethodParameterContent", "id", "ClassMethodParameter", "id");
            AddForeignKey("ClassMethodContent", "id", "ClassMethod", "id");
            AddForeignKey("ClassContent", "id", "Class", "id");
        }
    }
}
