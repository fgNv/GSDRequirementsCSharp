namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecaserelationcontent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "UseCaseEntitiesRelationContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        description = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCaseEntityRelation", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("UseCaseEntitiesRelationContent", "id", "UseCaseEntityRelation");
            DropIndex("UseCaseEntitiesRelationContent", new[] { "id" });
            DropTable("UseCaseEntitiesRelationContent");
        }
    }
}
