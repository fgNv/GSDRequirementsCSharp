namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecasediagramposition : DbMigration
    {
        public override void Up()
        {
            AddColumn("Actor", "X", c => c.Int(nullable: false));
            AddColumn("Actor", "Y", c => c.Int(nullable: false));
            AddColumn("UseCase", "X", c => c.Int(nullable: false));
            AddColumn("UseCase", "Y", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("UseCase", "Y");
            DropColumn("UseCase", "X");
            DropColumn("Actor", "Y");
            DropColumn("Actor", "X");
        }
    }
}
