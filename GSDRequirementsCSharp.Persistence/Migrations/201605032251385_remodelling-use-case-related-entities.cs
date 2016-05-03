namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remodellingusecaserelatedentities : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Actor");
            DropForeignKey("UserCase", "SpecificationItem_Id", "SpecificationItem");
            DropIndex("UserCase", new[] { "SpecificationItem_Id" });
            CreateTable(
                "UseCaseEntity",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                        use_case_diagram_id = c.Guid(nullable: false),
                        UseCaseDiagram_Id1 = c.Guid(nullable: false),
                        UseCaseDiagram_Version1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id1, t.UseCaseDiagram_Version1 }, cascadeDelete: true)
                .ForeignKey("Actor", t => t.id)
                .ForeignKey("UseCase", t => t.id)
                .Index(t => t.id)
                .Index(t => new { t.UseCaseDiagram_Id1, t.UseCaseDiagram_Version1 });
            
            CreateTable(
                "ActorContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, unicode: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        actor_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("Actor", t => t.actor_id)
                .Index(t => t.actor_id);
            
            CreateTable(
                "UseCaseDiagram",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        version = c.Int(nullable: false),
                        specification_item_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.version })                
                .ForeignKey("SpecificationItem", t => t.specification_item_id, cascadeDelete: true)
                .Index(t => t.specification_item_id);
            
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
                "UseCaseEntityRelation",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        use_case_diagram_id = c.Guid(nullable: false),
                        source_id = c.Guid(nullable: false),
                        target_id = c.Guid(nullable: false),
                        UseCaseDiagram_Id = c.Guid(nullable: false),
                        UseCaseDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCaseEntity", t => t.source_id, cascadeDelete: true)
                .ForeignKey("UseCaseEntity", t => t.target_id, cascadeDelete: true)
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version }, cascadeDelete: true)
                .Index(t => t.source_id)
                .Index(t => t.target_id)
                .Index(t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version });
            
            CreateTable(
                "UseCaseContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        use_case_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCase", t => t.use_case_id)
                .Index(t => t.use_case_id);
            
            CreateTable(
                "UseCasesRelation",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        type = c.Int(nullable: false),
                        use_case_diagram_id = c.Guid(nullable: false),
                        source_id = c.Guid(nullable: false),
                        target_id = c.Guid(nullable: false),
                        UseCaseDiagram_Id = c.Guid(nullable: false),
                        UseCaseDiagram_Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCase", t => t.source_id)
                .ForeignKey("UseCase", t => t.target_id)
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version }, cascadeDelete: true)
                .Index(t => t.source_id)
                .Index(t => t.target_id)
                .Index(t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version });
            
            CreateTable(
                "UseCase",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        UseCaseDiagram_Id = c.Guid(),
                        UseCaseDiagram_Version = c.Int(),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCaseEntity", t => t.id)
                .ForeignKey("UseCaseDiagram", t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version })
                .Index(t => t.id)
                .Index(t => new { t.UseCaseDiagram_Id, t.UseCaseDiagram_Version });
            
            AddColumn("Actor", "UseCaseDiagram_Id", c => c.Guid());
            AddColumn("Actor", "UseCaseDiagram_Version", c => c.Int());
            AddPrimaryKey("Actor", "id");
            CreateIndex("Actor", "id");
            CreateIndex("Actor", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            AddForeignKey("Actor", "id", "UseCaseEntity", "id");
            AddForeignKey("Actor", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram", new[] { "id", "version" });
            DropColumn("Actor", "locale");
            DropColumn("Actor", "name");
            DropTable("UserCase");
        }
        
        public override void Down()
        {
            CreateTable(
                "UserCase",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(nullable: false, unicode: false, storeType: "text"),
                        actor_id = c.Guid(),
                        SpecificationItem_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)                ;
            
            AddColumn("Actor", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("Actor", "locale", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropForeignKey("UseCaseEntity", "id", "UseCase");
            DropForeignKey("UseCase", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("UseCase", "id", "UseCaseEntity");
            DropForeignKey("UseCaseEntity", "id", "Actor");
            DropForeignKey("Actor", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("Actor", "id", "UseCaseEntity");
            DropForeignKey("UseCasesRelation", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("UseCasesRelation", "target_id", "UseCase");
            DropForeignKey("UseCasesRelation", "source_id", "UseCase");
            DropForeignKey("UseCaseDiagram", "specification_item_id", "SpecificationItem");
            DropForeignKey("UseCaseEntityRelation", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("UseCaseEntityRelation", "target_id", "UseCaseEntity");
            DropForeignKey("UseCaseEntityRelation", "source_id", "UseCaseEntity");
            DropForeignKey("UseCaseContent", "use_case_id", "UseCase");
            DropForeignKey("UseCaseEntity", new[] { "UseCaseDiagram_Id1", "UseCaseDiagram_Version1" }, "UseCaseDiagram");
            DropForeignKey("UseCaseDiagramContent", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" }, "UseCaseDiagram");
            DropForeignKey("ActorContent", "actor_id", "Actor");
            DropIndex("UseCase", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("UseCase", new[] { "id" });
            DropIndex("Actor", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("Actor", new[] { "id" });
            DropIndex("UseCasesRelation", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("UseCasesRelation", new[] { "target_id" });
            DropIndex("UseCasesRelation", new[] { "source_id" });
            DropIndex("UseCaseContent", new[] { "use_case_id" });
            DropIndex("UseCaseEntityRelation", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("UseCaseEntityRelation", new[] { "target_id" });
            DropIndex("UseCaseEntityRelation", new[] { "source_id" });
            DropIndex("UseCaseDiagramContent", new[] { "UseCaseDiagram_Id", "UseCaseDiagram_Version" });
            DropIndex("UseCaseDiagram", new[] { "specification_item_id" });
            DropIndex("ActorContent", new[] { "actor_id" });
            DropIndex("UseCaseEntity", new[] { "UseCaseDiagram_Id1", "UseCaseDiagram_Version1" });
            DropIndex("UseCaseEntity", new[] { "id" });
            DropPrimaryKey("Actor");
            DropColumn("Actor", "UseCaseDiagram_Version");
            DropColumn("Actor", "UseCaseDiagram_Id");
            DropTable("UseCase");
            DropTable("UseCasesRelation");
            DropTable("UseCaseContent");
            DropTable("UseCaseEntityRelation");
            DropTable("UseCaseDiagramContent");
            DropTable("UseCaseDiagram");
            DropTable("ActorContent");
            DropTable("UseCaseEntity");
            CreateIndex("UserCase", "SpecificationItem_Id");
            AddForeignKey("UserCase", "SpecificationItem_Id", "SpecificationItem", "id", cascadeDelete: true);
            AddPrimaryKey("Actor", new[] { "id", "locale" });
        }
    }
}
