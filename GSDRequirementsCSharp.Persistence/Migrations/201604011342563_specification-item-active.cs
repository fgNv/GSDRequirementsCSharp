namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specificationitemactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpecificationItem", "active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpecificationItem", "active");
        }
    }
}
