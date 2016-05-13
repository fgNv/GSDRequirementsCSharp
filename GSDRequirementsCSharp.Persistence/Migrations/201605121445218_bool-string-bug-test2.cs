namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolstringbugtest2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("UseCase", "is_last_version");
        }
        
        public override void Down()
        {
            AddColumn("UseCase", "is_last_version", c => c.Boolean(nullable: false));
        }
    }
}
