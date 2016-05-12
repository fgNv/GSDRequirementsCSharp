namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecasecontentnotrequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("UseCaseContent", "Path", c => c.String(maxLength: 65535, storeType: "nvarchar"));
            AlterColumn("UseCaseContent", "description", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("UseCaseContent", "description", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("UseCaseContent", "Path", c => c.String(nullable: false, maxLength: 65535, storeType: "nvarchar"));
        }
    }
}
