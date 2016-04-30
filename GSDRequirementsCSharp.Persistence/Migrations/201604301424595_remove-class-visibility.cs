namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeclassvisibility : DbMigration
    {
        public override void Up()
        {
            DropColumn("Class", "visibility");
        }
        
        public override void Down()
        {
            AddColumn("Class", "visibility", c => c.Int(nullable: false));
        }
    }
}
