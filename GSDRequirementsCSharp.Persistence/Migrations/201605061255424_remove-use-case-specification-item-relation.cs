namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeusecasespecificationitemrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("UseCaseDiagram", "specification_item_id", "SpecificationItem");
            DropIndex("UseCaseDiagram", new[] { "specification_item_id" });
            DropColumn("UseCaseDiagram", "specification_item_id");
        }
        
        public override void Down()
        {
            AddColumn("UseCaseDiagram", "specification_item_id", c => c.Guid(nullable: false));
            CreateIndex("UseCaseDiagram", "specification_item_id");
            AddForeignKey("UseCaseDiagram", "specification_item_id", "SpecificationItem", "id", cascadeDelete: true);
        }
    }
}
