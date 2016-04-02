namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectContentControl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectContent", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.ProjectContent", "IsUpdated", c => c.Boolean(nullable: false));
            DropColumn("dbo.Project", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.ProjectContent", "IsUpdated");
            DropColumn("dbo.ProjectContent", "name");
        }
    }
}
