namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringspecificationitemrelations : DbMigration
    {
        public override void Up()
        {
            CreateIndex("UseCaseDiagram", "id");
            AddForeignKey("UseCaseDiagram", "id", "SpecificationItem", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("UseCaseDiagram", "id", "SpecificationItem");
            DropIndex("UseCaseDiagram", new[] { "id" });
        }
    }
}
