namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDbTables : DbMigration
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
                        PmName = c.String(),
                        ClientName = c.String(),
                        StartingDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ProjectName = c.String(),
                        ProjectNumber = c.String(),
                        TaskNumber = c.String(),
                        ClientLocation = c.String(),
                        DepartureLocation = c.String(),
                        Transportation = c.String(),
                        NeedOfPhone = c.Boolean(nullable: false),
                        NeedOfBankCard = c.Boolean(nullable: false),
                        Accommodation = c.String(storeType: "ntext"),
                        Comments = c.String(nullable: false, storeType: "ntext"),
                        Approved = c.Boolean(nullable: false),
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
                        Name = c.String(maxLength: 100),
                        Email = c.String(maxLength: 254),
                        Password = c.String(maxLength: 250),
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
