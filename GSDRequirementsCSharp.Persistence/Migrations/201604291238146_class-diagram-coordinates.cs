namespace GSDRequirementsCSharp.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classdiagramcoordinates : DbMigration
    {
        public override void Up()
        {
            AddColumn("Class", "coordinates_x", c => c.Int(nullable: false));
            AddColumn("Class", "coordinates_y", c => c.Int(nullable: false));
            CreateIndex("ClassRelationship", "source_id");
            CreateIndex("ClassRelationship", "target_id");
            AddForeignKey("ClassRelationship", "source_id", "Class", "id", cascadeDelete: true);
            AddForeignKey("ClassRelationship", "target_id", "Class", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("ClassRelationship", "target_id", "Class");
            DropForeignKey("ClassRelationship", "source_id", "Class");
            DropIndex("ClassRelationship", new[] { "target_id" });
            DropIndex("ClassRelationship", new[] { "source_id" });
            DropColumn("Class", "coordinates_y");
            DropColumn("Class", "coordinates_x");
        }
    }
}
