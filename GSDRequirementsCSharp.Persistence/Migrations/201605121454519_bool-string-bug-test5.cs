namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolstringbugtest5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("UseCase", "is_last_version", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("UseCase", "is_last_version");
        }
    }
}
