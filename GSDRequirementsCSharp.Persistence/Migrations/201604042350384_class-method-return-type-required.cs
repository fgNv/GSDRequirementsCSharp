namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classmethodreturntyperequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClassMethod", "return_type", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClassMethod", "return_type", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
