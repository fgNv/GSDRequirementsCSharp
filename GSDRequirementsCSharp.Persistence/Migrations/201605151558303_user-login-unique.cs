namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userloginunique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("User", "login", unique: true, name: "login_unique");
        }
        
        public override void Down()
        {
            DropIndex("User", "login_unique");
        }
    }
}
