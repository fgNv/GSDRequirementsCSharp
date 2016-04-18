namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specificationItemLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecificationItemSpecificationItem",
                c => new
                    {
                        SpecificationItem_Id = c.Guid(nullable: false),
                        SpecificationItem_Id1 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpecificationItem_Id, t.SpecificationItem_Id1 })
                .ForeignKey("dbo.SpecificationItem", t => t.SpecificationItem_Id)
                .ForeignKey("dbo.SpecificationItem", t => t.SpecificationItem_Id1)
                .Index(t => t.SpecificationItem_Id)
                .Index(t => t.SpecificationItem_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecificationItemSpecificationItem", "SpecificationItem_Id1", "dbo.SpecificationItem");
            DropForeignKey("dbo.SpecificationItemSpecificationItem", "SpecificationItem_Id", "dbo.SpecificationItem");
            DropIndex("dbo.SpecificationItemSpecificationItem", new[] { "SpecificationItem_Id1" });
            DropIndex("dbo.SpecificationItemSpecificationItem", new[] { "SpecificationItem_Id" });
            DropTable("dbo.SpecificationItemSpecificationItem");
        }
    }
}
