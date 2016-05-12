namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecaseprepostconditions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "UseCasePostCondition",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        UseCase_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCase", t => t.UseCase_Id)
                .Index(t => t.UseCase_Id);
            
            CreateTable(
                "UseCasePostConditionContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        description = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCasePostCondition", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "UseCasePreCondition",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        UseCase_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("UseCase", t => t.UseCase_Id)
                .Index(t => t.UseCase_Id);
            
            CreateTable(
                "UseCasePreConditionContent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        locale = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        description = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.id, t.locale })                
                .ForeignKey("UseCasePreCondition", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            AddColumn("UseCaseContent", "Path", c => c.String(nullable: false, maxLength: 65535, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropForeignKey("UseCasePreCondition", "UseCase_Id", "UseCase");
            DropForeignKey("UseCasePreConditionContent", "id", "UseCasePreCondition");
            DropForeignKey("UseCasePostCondition", "UseCase_Id", "UseCase");
            DropForeignKey("UseCasePostConditionContent", "id", "UseCasePostCondition");
            DropIndex("UseCasePreConditionContent", new[] { "id" });
            DropIndex("UseCasePreCondition", new[] { "UseCase_Id" });
            DropIndex("UseCasePostConditionContent", new[] { "id" });
            DropIndex("UseCasePostCondition", new[] { "UseCase_Id" });
            DropColumn("UseCaseContent", "Path");
            DropTable("UseCasePreConditionContent");
            DropTable("UseCasePreCondition");
            DropTable("UseCasePostConditionContent");
            DropTable("UseCasePostCondition");
        }
    }
}
