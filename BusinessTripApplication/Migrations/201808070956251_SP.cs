namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "EndDate", c => c.DateTime());
            CreateStoredProcedure(
                "dbo.Trip_Insert",
                p => new
                    {
                        PmName = p.String(),
                        ClientName = p.String(),
                        StartingDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        ProjectName = p.String(),
                        ProjectNumber = p.String(),
                        TaskName = p.String(),
                        TaskNumber = p.String(),
                        ClientLocation = p.String(),
                        DepartureLocation = p.String(),
                        Transportation = p.String(),
                        NeedOfPhone = p.Boolean(),
                        NeedOfBankCard = p.Boolean(),
                        Accommodation = p.String(storeType: "ntext"),
                        Comments = p.String(storeType: "ntext"),
                        Status = p.Int(),
                        Area_Id = p.Int(),
                        User_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Trips]([PmName], [ClientName], [StartingDate], [EndDate], [ProjectName], [ProjectNumber], [TaskName], [TaskNumber], [ClientLocation], [DepartureLocation], [Transportation], [NeedOfPhone], [NeedOfBankCard], [Accommodation], [Comments], [Status], [Area_Id], [User_Id])
                      VALUES (@PmName, @ClientName, @StartingDate, @EndDate, @ProjectName, @ProjectNumber, @TaskName, @TaskNumber, @ClientLocation, @DepartureLocation, @Transportation, @NeedOfPhone, @NeedOfBankCard, @Accommodation, @Comments, @Status, @Area_Id, @User_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Trips]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Trips] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Trip_Update",
                p => new
                    {
                        Id = p.Int(),
                        PmName = p.String(),
                        ClientName = p.String(),
                        StartingDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        ProjectName = p.String(),
                        ProjectNumber = p.String(),
                        TaskName = p.String(),
                        TaskNumber = p.String(),
                        ClientLocation = p.String(),
                        DepartureLocation = p.String(),
                        Transportation = p.String(),
                        NeedOfPhone = p.Boolean(),
                        NeedOfBankCard = p.Boolean(),
                        Accommodation = p.String(storeType: "ntext"),
                        Comments = p.String(storeType: "ntext"),
                        Status = p.Int(),
                        Area_Id = p.Int(),
                        User_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Trips]
                      SET [PmName] = @PmName, [ClientName] = @ClientName, [StartingDate] = @StartingDate, [EndDate] = @EndDate, [ProjectName] = @ProjectName, [ProjectNumber] = @ProjectNumber, [TaskName] = @TaskName, [TaskNumber] = @TaskNumber, [ClientLocation] = @ClientLocation, [DepartureLocation] = @DepartureLocation, [Transportation] = @Transportation, [NeedOfPhone] = @NeedOfPhone, [NeedOfBankCard] = @NeedOfBankCard, [Accommodation] = @Accommodation, [Comments] = @Comments, [Status] = @Status, [Area_Id] = @Area_Id, [User_Id] = @User_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Trip_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Area_Id = p.Int(),
                        User_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Trips]
                      WHERE ((([Id] = @Id) AND (([Area_Id] = @Area_Id) OR ([Area_Id] IS NULL AND @Area_Id IS NULL))) AND (([User_Id] = @User_Id) OR ([User_Id] IS NULL AND @User_Id IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Trip_Delete");
            DropStoredProcedure("dbo.Trip_Update");
            DropStoredProcedure("dbo.Trip_Insert");
            AlterColumn("dbo.Trips", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
