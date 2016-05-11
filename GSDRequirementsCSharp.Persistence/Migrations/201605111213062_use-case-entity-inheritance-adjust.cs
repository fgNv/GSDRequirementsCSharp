namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usecaseentityinheritanceadjust : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Actor", "UseCaseEntity_Id", "UseCaseEntity");
            DropForeignKey("UseCase", "UseCaseEntity_Id", "UseCaseEntity");
            DropIndex("Actor", new[] { "UseCaseEntity_Id" });
            DropIndex("UseCase", new[] { "UseCaseEntity_Id" });
            DropColumn("Actor", "UseCaseEntity_Id");
            DropColumn("UseCase", "UseCaseEntity_Id");
        }
        
        public override void Down()
        {
            AddColumn("UseCase", "UseCaseEntity_Id", c => c.Guid(nullable: false));
            AddColumn("Actor", "UseCaseEntity_Id", c => c.Guid(nullable: false));
            CreateIndex("UseCase", "UseCaseEntity_Id");
            CreateIndex("Actor", "UseCaseEntity_Id");
            AddForeignKey("UseCase", "UseCaseEntity_Id", "UseCaseEntity", "id");
            AddForeignKey("Actor", "UseCaseEntity_Id", "UseCaseEntity", "id");
        }
    }
}
