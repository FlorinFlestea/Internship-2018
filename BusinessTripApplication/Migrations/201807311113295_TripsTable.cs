namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TripsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Trips",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PmName = c.String(nullable: false),
                    ClientName = c.String(nullable: false),
                    StartingDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    ProjectName = c.String(),
                    ProjectNumber = c.String(nullable: false),
                    TaskName = c.String(),
                    TaskNumber = c.String(nullable: false),
                    ClientLocation = c.String(nullable: false),
                    DepartureLocation = c.String(nullable: false),
                    Transportation = c.String(nullable: false),
                    NeedOfPhone = c.Boolean(nullable: false),
                    NeedOfBankCard = c.Boolean(nullable: false),
                    Accommodation = c.String(nullable: false, storeType: "ntext"),
                    Comments = c.String(nullable: false, storeType: "ntext"),
                    Status = c.Int(nullable: false),
                    Area_Id = c.Int(),
                    User_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.Area_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Area_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Email = c.String(nullable: false, maxLength: 254),
                    Password = c.String(nullable: false, maxLength: 250),
                    IsEmailVerified = c.Boolean(nullable: false),
                    ActivationCode = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Trips", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Trips", "Area_Id", "dbo.Areas");
            DropIndex("dbo.Trips", new[] { "User_Id" });
            DropIndex("dbo.Trips", new[] { "Area_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Trips");
            DropTable("dbo.Areas");
        }
    }
}
