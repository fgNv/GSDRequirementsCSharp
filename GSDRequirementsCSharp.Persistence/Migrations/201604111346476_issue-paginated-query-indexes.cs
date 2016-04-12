namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issuepaginatedqueryindexes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SpecificationItem", "active", name: "IX_Specification_Active");
            CreateIndex("dbo.Issue", "concluded", name: "IX_Issue_Concluded");
            CreateIndex("dbo.Requirement", "is_last_version", name: "IX_Requirement_Last_Version");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Requirement", "IX_Requirement_Last_Version");
            DropIndex("dbo.Issue", "IX_Issue_Concluded");
            DropIndex("dbo.SpecificationItem", "IX_Specification_Active");
        }
    }
}
