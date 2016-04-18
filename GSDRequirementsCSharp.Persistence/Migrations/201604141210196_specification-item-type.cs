namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specificationitemtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpecificationItem", "type", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpecificationItem", "type");
        }
    }
}
