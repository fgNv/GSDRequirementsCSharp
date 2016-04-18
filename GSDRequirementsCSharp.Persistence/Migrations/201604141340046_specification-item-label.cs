namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specificationitemlabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpecificationItem", "label", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpecificationItem", "label");
        }
    }
}
