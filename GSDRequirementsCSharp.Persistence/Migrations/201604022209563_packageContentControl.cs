namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class packageContentControl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PackageContent", "is_updated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PackageContent", "is_updated");
        }
    }
}
