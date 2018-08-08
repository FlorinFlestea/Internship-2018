namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database : DbMigration
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
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
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
                        EndDate = c.DateTime(),
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
                        ActivationCodeExpireDate = c.DateTime(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Trips", "Area_Id", "dbo.Areas");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Trips", new[] { "User_Id" });
            DropIndex("dbo.Trips", new[] { "Area_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Trips");
            DropTable("dbo.Roles");
            DropTable("dbo.Areas");
        }
    }
}
