namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolstringbugtest3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("UseCase", "is_last_version2", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("UseCase", "is_last_version2");
        }
    }
}
