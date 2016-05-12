namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolstringbugtest4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("UseCase", "is_last_version2");
        }
        
        public override void Down()
        {
            AddColumn("UseCase", "is_last_version2", c => c.Boolean(nullable: false));
        }
    }
}
