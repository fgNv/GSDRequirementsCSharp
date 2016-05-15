namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactemailunique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Contact", "email", unique: true, name: "email_unique");
        }
        
        public override void Down()
        {
            DropIndex("Contact", "email_unique");
        }
    }
}
