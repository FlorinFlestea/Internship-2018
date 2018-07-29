namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namearea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "Area_Id", "dbo.Areas");
            DropIndex("dbo.Trips", new[] { "Area_Id" });
            AlterColumn("dbo.Trips", "Area_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "Area_Id");
            AddForeignKey("dbo.Trips", "Area_Id", "dbo.Areas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "Area_Id", "dbo.Areas");
            DropIndex("dbo.Trips", new[] { "Area_Id" });
            AlterColumn("dbo.Trips", "Area_Id", c => c.Int());
            CreateIndex("dbo.Trips", "Area_Id");
            AddForeignKey("dbo.Trips", "Area_Id", "dbo.Areas", "Id");
        }
    }
}
