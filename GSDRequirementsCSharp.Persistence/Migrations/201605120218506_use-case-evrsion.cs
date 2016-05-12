namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecaseevrsion : DbMigration
    {
        public override void Up()
        {
            DropIndex("UseCase", "use_case_identifier");
            AddColumn("UseCase", "version", c => c.Int(nullable: false));
            CreateIndex("UseCase", new[] { "identifier", "project_id", "version" }, unique: true, name: "use_case_identifier");
        }
        
        public override void Down()
        {
            DropIndex("UseCase", "use_case_identifier");
            DropColumn("UseCase", "version");
            CreateIndex("UseCase", new[] { "identifier", "project_id" }, unique: true, name: "use_case_identifier");
        }
    }
}
