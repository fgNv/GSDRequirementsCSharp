namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecasespecificationitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("UseCase", "identifier", c => c.Int(nullable: false));
            AddColumn("UseCase", "specification_item_id", c => c.Guid(nullable: false));
            AddColumn("UseCase", "project_id", c => c.Guid(nullable: false));
            CreateIndex("UseCaseDiagram", new[] { "identifier", "project_id", "version" }, unique: true, name: "use_case_diagram_identifier");
            CreateIndex("ClassDiagram", new[] { "identifier", "project_id", "version" }, unique: true, name: "class_diagram_identifier");
            CreateIndex("UseCase", new[] { "identifier", "project_id" }, unique: true, name: "use_case_identifier");
            CreateIndex("UseCase", "specification_item_id");
            AddForeignKey("UseCase", "specification_item_id", "SpecificationItem", "id", cascadeDelete: true);
            AddForeignKey("UseCase", "project_id", "Project", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("UseCase", "project_id", "Project");
            DropForeignKey("UseCase", "specification_item_id", "SpecificationItem");
            DropIndex("UseCase", new[] { "specification_item_id" });
            DropIndex("UseCase", "use_case_identifier");
            DropIndex("ClassDiagram", "class_diagram_identifier");
            DropIndex("UseCaseDiagram", "use_case_diagram_identifier");
            DropColumn("UseCase", "project_id");
            DropColumn("UseCase", "specification_item_id");
            DropColumn("UseCase", "identifier");
        }
    }
}
