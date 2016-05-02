namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeactiveduplicatedflag : DbMigration
    {
        public override void Up()
        {
            DropColumn("ClassDiagram", "active");
        }
        
        public override void Down()
        {
            AddColumn("ClassDiagram", "active", c => c.Boolean(nullable: false));
        }
    }
}
